namespace Aardvark.WebAssembly

open Aardvark.Base
open Aardvark.WebAssembly
open System.Runtime.CompilerServices

//[<RequireQualifiedAccess; Struct>]
type internal HalfRangeKind =
    | Open = 0
    | Close = 1

[<Struct; CustomEquality; CustomComparison>]
type RangeSet<'a when 'a : comparison> internal(store : MapExt<'a, HalfRangeKind>) =

    member internal x.Store = store

    member x.Count = store.Count / 2

    member x.Add(minInclusive : 'a, maxExclusive : 'a) =
        store |> MapExt.replaceRange minInclusive maxExclusive (fun l r ->
            match l with
            | ValueSome struct(_, HalfRangeKind.Open) ->
                match r with
                | ValueSome struct(_, HalfRangeKind.Close) ->
                    struct(ValueNone, ValueNone)
                | _ ->
                    struct(ValueNone, ValueSome HalfRangeKind.Close)

            | _ ->
                match r with
                | ValueSome struct(_, HalfRangeKind.Close) ->
                    struct(ValueSome HalfRangeKind.Open, ValueNone)
                | _ ->
                    struct(ValueSome HalfRangeKind.Open, ValueSome HalfRangeKind.Close)
        )
        |> RangeSet
    
    member x.Remove(minInclusive : 'a, maxExclusive : 'a) =
        store |> MapExt.replaceRange minInclusive maxExclusive (fun l r ->
            match l with
            | ValueSome struct(_, HalfRangeKind.Open) ->
                match r with
                | ValueSome struct(_, HalfRangeKind.Close) ->
                    struct(ValueSome HalfRangeKind.Close, ValueSome HalfRangeKind.Open)
                | _ ->
                    struct(ValueSome HalfRangeKind.Close, ValueNone)

            | _ ->
                match r with
                | ValueSome struct(_, HalfRangeKind.Close) ->
                    struct(ValueNone, ValueSome HalfRangeKind.Open)
                | _ ->
                    struct(ValueNone, ValueNone)
        )
        |> RangeSet
        
    member x.Contains(key : 'a) =
        let struct(l, s, _) = store.NeighboursV(key)
        match s with
        | ValueSome HalfRangeKind.Open -> true
        | _ ->
            match l with
            | ValueSome struct(_, HalfRangeKind.Open) -> true
            | _ -> false

    member x.Contains(minInclusive : 'a, maxExclusive : 'a) =
        let struct(l, minValue, r) = store.SplitV minInclusive
        let inner = r.WithMaxExclusive maxExclusive

        let opened = 
            match minValue with
            | ValueSome HalfRangeKind.Open -> true
            | ValueSome HalfRangeKind.Close -> false
            | _ -> 
                match l.TryMaxKeyValueV() with
                | ValueSome struct(_, HalfRangeKind.Open) -> true
                | _ -> false

        MapExt.isEmpty inner && opened

    member x.TryHead() =
        match store.TryRemoveMin() with
        | ValueSome struct(struct(lk, _), rest) ->
            match rest.TryMinKeyValueV() with
            | ValueSome struct(hk, _) ->
                Some struct(lk, hk)
            | _ ->
                None
        | _ ->
            None
            
    member x.TryLast() =
        match store.TryRemoveMax() with
        | ValueSome struct(struct(hk, _), rest) ->
            match rest.TryMaxKeyValueV() with
            | ValueSome struct(lk, _) ->
                Some struct(lk, hk)
            | _ ->
                None
        | _ ->
            None

    static member Empty = RangeSet(MapExt.empty)
    
    static member OfSeq(seq : seq<struct('a * 'a)>) =
        let mutable res = RangeSet<'a>.Empty
        for struct(l, h) in seq do res <- res.Add(l, h)
        res

    static member OfList(list : list<struct('a * 'a)>) =
        let mutable res = RangeSet<'a>.Empty
        for struct(l, h) in list do res <- res.Add(l, h)
        res

    static member OfArray(arr : struct('a * 'a)[]) =
        let mutable res = RangeSet<'a>.Empty
        for struct(l, h) in arr do res <- res.Add(l, h)
        res

    member x.ToSeq() =
        RangeSetEnumerable(store) :> seq<_>

    member x.ToList() =
        let rec create (l : list<struct('a * HalfRangeKind)>) =
            match l with
            | [] -> []
            | struct(l, _) :: struct(h, _) :: rest ->
                struct(l, h) :: create rest
            | _ ->
                failwith "inconsistent"
        store |> MapExt.toListV |> create

    member x.ToArray() =    
        let arr = MapExt.toArrayV store
        let res = Array.zeroCreate (arr.Length / 2)
        let mutable i = 0
        for o in 0 .. res.Length - 1 do
            let struct(min,_) = arr.[i]
            let struct(max,_) = arr.[i+1]
            res.[o] <- struct(min, max)
            i <- i + 2
        res
    member x.CopyTo(dst : struct('a * 'a)[], startIndex : int) =
        if startIndex < 0 || startIndex + x.Count > dst.Length then raise <| System.IndexOutOfRangeException()
        let arr = MapExt.toArrayV store
        let mutable i = 0
        let mutable o = startIndex
        for _ in 0 .. x.Count - 1 do
            let struct(min,_) = arr.[i]
            let struct(max,_) = arr.[i+1]
            dst.[o] <- struct(min, max)
            i <- i + 2
            o <- o + 1

    override x.ToString() =
        x.ToArray() 
        |> Seq.truncate 10 
        |> Seq.map (fun struct(l, h) -> sprintf "[%A,%A)" l h) 
        |> String.concat "; " 
        |> sprintf "rangeset [%s]"

    override x.GetHashCode() =
        (0, store) ||> MapExt.fold (fun h k v -> HashCode.Combine(h,  HashCode.Combine(Unchecked.hash k, Unchecked.hash v)))
        
    override x.Equals(o : obj) =
        match o with
        | :? RangeSet<'a> as o ->
            store = o.Store
        | _ ->  
            false

    member x.CompareTo(other : RangeSet<'a>) =
        Seq.compareWith Unchecked.compare x other

    member x.GetEnumerator() = new RangeSetEnumerator<_>(store.GetEnumerator())

    interface System.IComparable with
        member x.CompareTo o =
            x.CompareTo (o :?> RangeSet<'a>)
            
    interface System.IComparable<RangeSet<'a>> with
        member x.CompareTo o = x.CompareTo o

    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = new RangeSetEnumerator<_>(store.GetEnumerator()) :> _
  
    interface System.Collections.Generic.IEnumerable<struct('a * 'a)> with
        member x.GetEnumerator() = new RangeSetEnumerator<_>(store.GetEnumerator()) :> _

    interface System.Collections.Generic.IReadOnlyCollection<struct('a * 'a)> with
        member x.Count = x.Count
        
    interface System.Collections.Generic.ICollection<struct('a * 'a)> with
        member x.IsReadOnly = true
        member x.Count = x.Count
        member x.Contains(struct(min, max)) = x.Contains(min, max)
        member x.CopyTo(dst : struct('a * 'a)[], startIndex : int) = x.CopyTo(dst, startIndex)
        member x.Add _ = failwith "readonly"
        member x.Remove _ = failwith "readonly"
        member x.Clear() = failwith "readonly"

and RangeSetEnumerator<'a> =
    struct
        val mutable internal Inner : MapExtEnumerator<'a, HalfRangeKind>
        val mutable internal Value : struct('a * 'a)

        member x.Reset() =
            x.Inner.Reset()
            x.Value <- Unchecked.defaultof<_>

        member x.MoveNext() =
            if x.Inner.MoveNext() then
                let (KeyValue(l,_)) = x.Inner.Current
                if x.Inner.MoveNext() then
                    let (KeyValue(r, _)) = x.Inner.Current
                    x.Value <- struct(l, r)
                    true
                else
                    false
            else
                false

        member x.Current = x.Value

        interface System.Collections.IEnumerator with
            member x.MoveNext() = x.MoveNext()
            member x.Current = x.Current :> obj
            member x.Reset() = x.Reset()

        interface System.Collections.Generic.IEnumerator<struct('a * 'a)> with
            member x.Current = x.Current
            member x.Dispose() = 
                x.Inner.Dispose()
                x.Value <- Unchecked.defaultof<_>

        internal new(inner : MapExtEnumerator<'a, HalfRangeKind>) =
            {
                Inner = inner
                Value = Unchecked.defaultof<_>
            }
    end

and internal RangeSetEnumerable<'a when 'a : comparison> internal(store : MapExt<'a, HalfRangeKind>) =

    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = new RangeSetEnumerator<_>(store.GetEnumerator()) :> _
  
    interface System.Collections.Generic.IEnumerable<struct('a * 'a)> with
        member x.GetEnumerator() = new RangeSetEnumerator<_>(store.GetEnumerator()) :> _


[<AbstractClass; Sealed; Extension>]
type RangeSetExtensions private() =
    [<Extension>]
    static member Add(this : RangeSet<int>, range : Range1i) =
        this.Add(range.Min, range.Max + 1)

    [<Extension>]
    static member Add(this : RangeSet<int64>, range : Range1l) =
        this.Add(range.Min, range.Max + 1L)
        
    [<Extension>]
    static member Add(this : RangeSet<float32>, range : Range1f) =
        this.Add(range.Min, range.Max)

    [<Extension>]
    static member Add(this : RangeSet<float>, range : Range1d) =
        this.Add(range.Min, range.Max)



module RangeSet =
    [<GeneralizableValue>]
    let inline empty<'a when 'a : comparison> = RangeSet<'a>.Empty

    let inline add (minInclusive : 'a) (maxExclusive : 'a) (set : RangeSet<'a>) = set.Add(minInclusive, maxExclusive)
    let inline remove (minInclusive : 'a) (maxExclusive : 'a) (set : RangeSet<'a>) = set.Remove(minInclusive, maxExclusive)

    let inline ofSeq (elements : seq<struct('a * 'a)>) = RangeSet.OfSeq elements
    let inline ofList (elements : list<struct('a * 'a)>) = RangeSet.OfSeq elements
    let inline ofArray (elements : struct('a * 'a)[]) = RangeSet.OfArray elements

    let inline toSeq (set : RangeSet<'a>) = set.ToSeq()
    let inline toList (set : RangeSet<'a>) = set.ToList()
    let inline toArray (set : RangeSet<'a>) = set.ToArray()


