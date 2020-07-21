namespace rec Aardvark.WebAssembly

open System
open WebAssembly
open System.Threading.Tasks

type EventPhase =
    | None = 0
    | Capture = 1
    | At = 2
    | Bubble = 3
    
type MouseButton =
    | Main = 0
    | Auxilary = 1
    | Secondary = 2
    | Button4 = 3
    | Button5 = 4
    
[<Flags>]
type MouseButtons =
    | None = 0
    | Main = 1
    | Secondary = 2
    | Auxilary = 4
    | Button4 = 8
    | Button5 = 16
    
type NodeType =
    | Unknown = 0
    | Element = 1
    | Attribute = 2
    | Text = 3
    | CDataSection = 4
    | EntityReference = 5
    | Entity = 6
    | ProcessingInstruction = 7
    | Comment = 8
    | Document = 9
    | DocumentType = 10
    | DocumentFragment = 11
    | Notation = 12
    
[<Flags>]
type DocumentPosition =
    | Disconnected = 1
    | Preceding = 2
    | Following = 4
    | Contains = 8
    | ContainedBy = 16
    | ImplementationSpecific = 32

[<RequireQualifiedAccess>]
type PointerType =
    | Mouse
    | Pen
    | Touch
    | Other of name : string

type WheelDeltaMode =
    | Pixel = 0
    | Line = 1
    | Page = 2
    | Unknown = 3

type CloseReason =
    | Normal = 1000
    | GoingAway = 1001
    | ProtocolError = 1002
    | UnsupportedData = 1003
    | NoStatusReceived = 1005
    | Abnormal = 1006
    | InvalidFramePayload = 1007
    | PolicyViolation = 1008
    | MessageTooBig = 1009
    | MissingExtension = 1010
    | InternalError = 1011
    | ServiceRestart = 1012
    | TryAgainLater = 1013
    | BadGateway = 1014
    | TlsHandshake = 1015

type KeyboardLocation =
    | Standard = 0
    | Left = 1
    | Right = 2
    | NumPad = 3
    | Mobile = 4
    | JoyStick = 5
    
[<RequireQualifiedAccess>]
type InsertMode =
    | BeforeBegin
    | AfterBegin
    | BeforeEnd
    | AfterEnd
    
[<RequireQualifiedAccess>]
type ScrollBehaviour =
    | Auto
    | Smooth


/// The Event interface represents an event which takes place in the DOM.
[<AllowNullLiteral>]
type Event(r : JSObject) =
    inherit JsObj(r)

    /// The bubbles read-only property of the Event interface indicates whether the event bubbles up through the DOM or not.
    member x.Bubbles = r.GetObjectProperty("bubbles") |> unbox<bool>

    /// The cancelBubble property of the Event interface is a historical alias to Event.stopPropagation(). Setting its value to true before returning from an event handler prevents propagation of the event. In later implementations, setting this to false does nothing. See Browser compatibility for details.
    member x.CancelBubble
        with get() = r.GetObjectProperty("cancelBubble") |> unbox<bool>
        and set (v : bool) = r.SetObjectProperty("cancelBubble", v)

    /// The cancelable read-only property of the Event interface indicates whether the event can be canceled, and therefore prevented as if the event never happened. If the event is not cancelable, then its cancelable property will be false and the event listener cannot stop the event from occurring.
    member x.Cancelable : bool =
        r.GetObjectProperty("cancelable") |> unbox

    /// The read-only composed property of the Event interface returns a Boolean which indicates whether or not the event will propagate across the shadow DOM boundary into the standard DOM.
    member x.Composed : bool =
        r.GetObjectProperty("composed") |> unbox

    /// The currentTarget read-only property of the Event interface identifies the current target for the event, as the event traverses the DOM. It always refers to the element to which the event handler has been attached, as opposed to Event.target, which identifies the element on which the event occurred and which may be its descendant.
    member x.CurrentTarget : EventTarget =
        let o = r.GetObjectProperty("currentTarget")
        if isNull o then null
        else o |> unbox<JSObject> |> EventTarget
        
    /// The composedPath() method of the Event interface returns the event’s path which is an array of the objects on which listeners will be invoked. This does not include nodes in shadow trees if the shadow root was created with its ShadowRoot.mode closed.
    member x.ComposedPath : EventTarget[] =
        let o = r.Invoke("composedPath") |> unbox<JSObject>
        if isNull o then 
            [||]
        else
            let l = o.GetObjectProperty("length") |> convert<int>
            Array.init l (fun i -> o.GetObjectProperty(string i) |> convert<EventTarget>)

    /// The defaultPrevented read-only property of the Event interface returns a Boolean indicating whether or not the call to Event.preventDefault() canceled the event.
    member x.DefaultPrevented : bool =
        r.GetObjectProperty("defaultPrevented") |> unbox

    /// The eventPhase read-only property of the Event interface indicates which phase of the event flow is currently being evaluated.
    member x.EventPhase : EventPhase =
        r.GetObjectProperty("eventPhase") |> unbox<int> |> unbox<EventPhase>

    /// The Event property returnValue indicates whether the default action for this event has been prevented or not. It is set to true by default, allowing the default action to occur. Setting this property to false prevents the default action.
    member x.ReturnValue
        with get() : bool = r.GetObjectProperty("returnValue") |> unbox
        and set(v : bool) = r.SetObjectProperty("returnValue", v)

    /// The target property of the Event interface is a reference to the object onto which the event was dispatched. It is different from Event.currentTarget when the event handler is called during the bubbling or capturing phase of the event.
    member x.Target =
        let o = r.GetObjectProperty("target")
        if isNull o then null
        else o |> unbox<JSObject> |> EventTarget

    /// The timeStamp read-only property of the Event interface returns the time (in milliseconds) at which the event was created.
    member x.TimeStamp = r.GetObjectProperty("timeStamp") |> convert<int>
        
    /// The type read-only property of the Event interface returns a string containing the event's type. It is set when the event is constructed and is the name commonly used to refer to the specific event, such as click, load, or error.
    member x.Type = r.GetObjectProperty("type") |> convert<string>

    /// The isTrusted read-only property of the Event interface is a Boolean that is true when the event was generated by a user action, and false when the event was created or modified by a script or dispatched via EventTarget.dispatchEvent().
    member x.IsTrusted : bool = r.GetObjectProperty("isTrusted") |> convert<bool>

    /// The Event interface's preventDefault() method tells the user agent that if the event does not get explicitly handled, its default action should not be taken as it normally would be. The event continues to propagate as usual, unless one of its event listeners calls stopPropagation() or stopImmediatePropagation(), either of which terminates propagation at once.
    member x.PreventDefault() =
        r.Invoke("preventDefault") |> ignore

    /// The stopPropagation() method of the Event interface prevents further propagation of the current event in the capturing and bubbling phases. It does not, however, prevent any default behaviors from occurring; for instance, clicks on links are still processed. If you want to stop those behaviors, see the preventDefault() method.
    member x.StopPropagation() =
        r.Invoke("stopPropagation") |> ignore

    /// The stopImmediatePropagation() method of the Event interface prevents other listeners of the same event from being called.
    member x.StopImmediatePropagation() =
        r.Invoke("stopImmediatePropagation") |> ignore

    new (o : JsObj) =
        Event o.Reference

    new (typ : string, ?bubbles: bool, ?cancelable : bool, ?composed : bool) =
        let info =
            createObj [
                match bubbles with
                | Some b -> "bubbles", b :> obj
                | None -> ()
                match cancelable with
                | Some c -> "cancelable", c :> obj
                | None -> ()
                match composed with
                | Some c -> "composed", c :> obj
                | None -> ()
            ]
        Event(newObj "Event" [| typ :> obj; info :> obj |])

    new (typ : string) =
        Event(newObj "Event" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        Event(newObj "Event" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        Event(newObj "Event" [| typ :> obj; createObj info |])

/// The ExtendableEvent interface extends the lifetime of the install and activate events dispatched on the global scope as part of the service worker lifecycle. This ensures that any functional events (like FetchEvent) are not dispatched until it upgrades database schemas and deletes the outdated cache entries.
[<AllowNullLiteral>]
type ExtendableEvent(r : JSObject) =
    inherit Event(r)
    
    /// The ExtendableEvent.waitUntil() method tells the event dispatcher that work is ongoing. It can also be used to detect whether that work was successful. In service workers, waitUntil() tells the browser that work is ongoing until the promise settles, and it shouldn't terminate the service worker if it wants that work to complete.
    member x.WaitUntil(task : Task) =
        r.Invoke("waitUntil", js task) |> ignore

    new (o : JsObj) =
        ExtendableEvent o.Reference

    new (typ : string) =
        ExtendableEvent(newObj "ExtendableEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        ExtendableEvent(newObj "ExtendableEvent" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        ExtendableEvent(newObj "ExtendableEvent" [| typ :> obj; createObj info |])

/// The AnimationEvent interface represents events providing information related to animations.
[<AllowNullLiteral>]
type AnimationEvent(r : JSObject) =
    inherit Event(r)
    
    /// The AnimationEvent.animationName read-only property is a DOMString containing the value of the animation-name CSS property associated with the transition.
    member x.AnimationName = r.GetObjectProperty("animationName") |> unbox<string>

    /// The AnimationEvent.elapsedTime read-only property is a float giving the amount of time the animation has been running, in seconds, when this event fired, excluding any time the animation was paused. For an animationstart event, elapsedTime is 0.0 unless there was a negative value for animation-delay, in which case the event will be fired with elapsedTime containing (-1 * delay).
    member x.ElapsedTime = r.GetObjectProperty("elapsedTime") |> convert<float>

    /// The AnimationEvent.pseudoElement read-only property is a DOMString, starting with '::', containing the name of the pseudo-element the animation runs on. If the animation doesn't run on a pseudo-element but on the element, an empty string: ''.
    member x.PseudoElement = r.GetObjectProperty("pseudoElement") |> unbox<string>

    new (o : JsObj) =
        AnimationEvent o.Reference

    new (typ : string) =
        AnimationEvent(newObj "AnimationEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        AnimationEvent(newObj "AnimationEvent" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        AnimationEvent(newObj "AnimationEvent" [| typ :> obj; createObj info |])

/// The BlobEvent interface represents events associated with a Blob. These blobs are typically, but not necessarily,  associated with media content.
[<AllowNullLiteral>]
type BlobEvent(r : JSObject) =
    inherit Event(r)

    /// The BlobEvent.data read-only property represents a Blob associated with the event.
    member x.Data = r.GetObjectProperty("data") |> convert<Blob>

    /// The timecode readonlyinline property of the BlobEvent interface a DOMHighResTimeStamp indicating the difference between the timestamp of the first chunk in data, and the timestamp of the first chunk in the first BlobEvent produced by this recorder. Note that the timecode in the first produced BlobEvent does not need to be zero.
    member x.Timecode = r.GetObjectProperty("timeCode") |> convert<float>

    new (o : JsObj) =
        BlobEvent o.Reference

    new (typ : string) =
        BlobEvent(newObj "BlobEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        BlobEvent(newObj "BlobEvent" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        BlobEvent(newObj "BlobEvent" [| typ :> obj; createObj info |])

/// The ClipboardEvent interface represents events providing information related to modification of the clipboard, that is cut, copy, and paste events.
[<AllowNullLiteral>]
type ClipboardEvent(r : JSObject) =
    inherit Event(r)

    /// The ClipboardEvent.clipboardData property holds a DataTransfer object.
    member x.ClipboardData = r.GetObjectProperty "clipboardData" |> convert<DataTransfer>

    new (o : JsObj) =
        ClipboardEvent o.Reference

    new (typ : string) =
        ClipboardEvent(newObj "ClipboardEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        ClipboardEvent(newObj "ClipboardEvent" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        ClipboardEvent(newObj "ClipboardEvent" [| typ :> obj; createObj info |])

/// The UIEvent interface represents simple user interface events.
[<AllowNullLiteral>]
type UIEvent(r : JSObject) =
    inherit Event(r)

    /// The UIEvent.detail read-only property, when non-zero, provides the current (or next, depending on the event) click count.
    member x.Detail = r.GetObjectProperty("detail") |> convert<int>
    
    /// The UIEvent.layerX read-only property returns the horizontal coordinate of the event relative to the current layer.
    member x.LayerX =
        r.GetObjectProperty("layerX") |> System.Convert.ToDouble

    /// The UIEvent.layerY read-only property returns the vertical coordinate of the event relative to the current layer.
    member x.LayerY =
        r.GetObjectProperty("layerY") |> System.Convert.ToDouble
        
    /// The UIEvent.sourceCapabilities read-only property returns an instance of the InputDeviceCapabilities interface which provides information about the physical device responsible for generating a touch event. If no input device was responsible for the event, it returns null.
    member x.SourceCapabilities =
        r.GetObjectProperty("sourceCapabilities") |> convert<JsObj>

    /// The UIEvent.view read-only property returns the WindowProxy object from which the event was generated. In browsers, this is the Window object the event happened in.
    member x.View =
        r.GetObjectProperty("view") |> convert<JsObj>

    new(r : JsObj) =
        UIEvent(r.Reference)
        
    new (typ : string, ?detail: int, ?view : JsObj, ?sourceCapabilities : bool, ?bubbles: bool, ?cancelable : bool, ?composed : bool) =
        let info =
            createObj [
                match detail with
                | Some d -> "detail", d :> obj
                | None -> ()
                match view with
                | Some c -> "view", c :> obj
                | None -> ()
                match sourceCapabilities with
                | Some c -> "sourceCapabilities", c :> obj
                | None -> ()
                match bubbles with
                | Some b -> "bubbles", b :> obj
                | None -> ()
                match cancelable with
                | Some c -> "cancelable", c :> obj
                | None -> ()
                match composed with
                | Some c -> "composed", c :> obj
                | None -> ()
            ]
        UIEvent(newObj "UIEvent" [| typ :> obj; info :> obj |])

    new (typ : string) =
        UIEvent(newObj "UIEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        UIEvent(newObj "UIEvent" [| typ :> obj; info |])

    new (typ : string, info : list<string * obj>) =
        UIEvent(newObj "UIEvent" [| typ :> obj; createObj info |])

/// The MouseEvent interface represents events that occur due to the user interacting with a pointing device (such as a mouse). Common events using this interface include click, dblclick, mouseup, mousedown.
[<AllowNullLiteral>]
type MouseEvent(r : JSObject) =
    inherit UIEvent(r)
    
    /// The MouseEvent.altKey read-only property is a Boolean that indicates whether the alt key was pressed or not when a given mouse event occurs.
    member x.Alt =
        r.GetObjectProperty("altKey") |> unbox<bool>
        
    /// The MouseEvent.ctrlKey read-only property is a Boolean that indicates whether the ctrl key was pressed or not when a given mouse event occurs.
    member x.Ctrl =
        r.GetObjectProperty("ctrlKey") |> unbox<bool>
        
    /// The MouseEvent.metaKey read-only property is a Boolean that indicates whether the meta key was pressed or not when a given mouse event occurs.
    member x.Meta =
        r.GetObjectProperty("metaKey") |> unbox<bool>
        
    /// The MouseEvent.shiftKey read-only property is a Boolean that indicates whether the shift key was pressed or not when a given mouse event occurs.
    member x.Shift =
        r.GetObjectProperty("shiftKey") |> unbox<bool>
        
    /// The MouseEvent.button read-only property indicates which button was pressed on the mouse to trigger the event.
    member x.Button =
        r.GetObjectProperty("button") |> unbox<int> |> unbox<MouseButton>
        
    /// The MouseEvent.buttons read-only property indicates which buttons are pressed on the mouse (or other input device) when a mouse event is triggered.
    member x.Buttons =
        r.GetObjectProperty("buttons") |> unbox<int> |> unbox<MouseButtons>

    /// The clientX read-only property of the MouseEvent interface provides the horizontal coordinate within the application's client area at which the event occurred (as opposed to the coordinate within the page).
    member x.ClientX =
        r.GetObjectProperty("clientX") |> System.Convert.ToDouble

    /// The clientY read-only property of the MouseEvent interface provides the vertical coordinate within the application's client area at which the event occurred (as opposed to the coordinate within the page).
    member x.ClientY =
        r.GetObjectProperty("clientY") |> System.Convert.ToDouble
        
    /// The movementX read-only property of the MouseEvent interface provides the difference in the X coordinate of the mouse pointer between the given event and the previous mousemove event. In other words, the value of the property is computed like this: currentEvent.movementX = currentEvent.screenX - previousEvent.screenX.
    member x.MovementX =
        r.GetObjectProperty("movementX") |> System.Convert.ToDouble

    /// The movementY read-only property of the MouseEvent interface provides the difference in the Y coordinate of the mouse pointer between the given event and the previous mousemove event. In other words, the value of the property is computed like this: currentEvent.movementY = currentEvent.screenY - previousEvent.screenY.
    member x.MovementY =
        r.GetObjectProperty("movementY") |> System.Convert.ToDouble
        
    /// The offsetX read-only property of the MouseEvent interface provides the offset in the X coordinate of the mouse pointer between that event and the padding edge of the target node. 
    member x.OffsetX =
        r.GetObjectProperty("offsetX") |> System.Convert.ToDouble

    /// The offsetY read-only property of the MouseEvent interface provides the offset in the Y coordinate of the mouse pointer between that event and the padding edge of the target node.
    member x.OffsetY =
        r.GetObjectProperty("offsetY") |> System.Convert.ToDouble

    /// The pageX read-only property of the MouseEvent interface returns the X (horizontal) coordinate (in pixels) at which the mouse was clicked, relative to the left edge of the entire document. This includes any portion of the document not currently visible.
    member x.PageX =
        r.GetObjectProperty("pageX") |> System.Convert.ToDouble

    /// The pageY read-only property of the MouseEvent interface returns the Y (vertical) coordinate in pixels of the event relative to the whole document. This property takes into account any vertical scrolling of the page.
    member x.PageY =
        r.GetObjectProperty("pageY") |> System.Convert.ToDouble
        
    /// The screenX read-only property of the MouseEvent interface provides the horizontal coordinate (offset) of the mouse pointer in global (screen) coordinates.
    member x.ScreenX =
        r.GetObjectProperty("screenX") |> System.Convert.ToDouble

    /// The screenY read-only property of the MouseEvent interface provides the vertical coordinate (offset) of the mouse pointer in global (screen) coordinates.
    member x.ScreenY =
        r.GetObjectProperty("screenY") |> System.Convert.ToDouble
        
    /// A number representing a given button.
    member x.Which =
        r.GetObjectProperty("which") |> convert<int>

    /// The MouseEvent.x property is an alias for the MouseEvent.clientX property.
    member x.X = x.ClientX

    /// The MouseEvent.y property is an alias for the MouseEvent.clientY property.
    member x.Y = x.ClientY
        
    /// The MouseEvent.getModifierState() method returns the current state of the specified modifier key: true if the modifier is active (that is the modifier key is pressed or locked), otherwise, false.
    member x.GetModifierState(key : string) =
        r.Invoke("getModifierState", key) |> convert<bool>

    //member x.RelatedTarget =
    //    let t = r.GetObjectProperty("relatedTarget")
    //    if isNull t then null
    //    else EventTarget(unbox<JSObject> t)
        
    new (o : JsObj) =
        MouseEvent o.Reference

    new (typ : string) =
        MouseEvent(newObj "MouseEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        MouseEvent(newObj "MouseEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        MouseEvent(newObj "MouseEvent" [| typ :> obj; createObj info |])

/// The PointerEvent interface represents the state of a DOM event produced by a pointer such as the geometry of the contact point, the device type that generated the event, the amount of pressure that was applied on the contact surface, etc.
[<AllowNullLiteral>]
type PointerEvent(r : JSObject) =
    inherit MouseEvent(r)

    /// The pointerId read-only property of the PointerEvent interface is an identifier assigned to a given pointer event. The identifier is unique, being different from the identifiers of all other active pointer events. Since the value may be randomly generated, it is not guaranteed to convey any particular meaning.
    member x.PointerId = r.GetObjectProperty("pointerId") |> convert<int>
    
    /// The width read-only property of the PointerEvent interface represents the width of the pointer's contact geometry along the x-axis, measured in CSS pixels. Depending on the source of the pointer device (such as a finger), for a given pointer, each event may produce a different value.
    member x.Width = r.GetObjectProperty("width") |> convert<float>
    
    /// The height read-only property of the PointerEvent interface represents the height of the pointer's contact geometry, along the y-axis (in CSS pixels). Depending on the source of the pointer device (for example a finger), for a given pointer, each event may produce a different value.
    member x.Height = r.GetObjectProperty("height") |> convert<float>
    
    /// The pressure read-only property of the PointerEvent interface indicates the normalized pressure of the pointer input.
    member x.Pressure = r.GetObjectProperty("pressure") |> convert<float>
    
    /// The tangentialPressure read-only property of the PointerEvent interface represents the normalized tangential pressure of the pointer input (also known as barrel pressure or cylinder stress).
    member x.TangentialPressure = r.GetObjectProperty("tangentialPressure") |> convert<float>

    /// The tiltX read-only property of the PointerEvent interface is the angle (in degrees) between the Y-Z plane of the pointer and the screen. This property is typically only useful for a pen/stylus pointer type.
    member x.TiltX = r.GetObjectProperty("tiltX") |> convert<float>
    
    /// The tiltY read-only property of the PointerEvent interface is the angle (in degrees) between the X-Z plane of the pointer and the screen. This property is typically only useful for a pen/stylus pointer type.
    member x.TiltY = r.GetObjectProperty("tiltY") |> convert<float>

    /// The twist read-only property of the PointerEvent interface represents the clockwise rotation of the pointer (e.g., pen stylus) around its major axis, in degrees.
    member x.Twist = r.GetObjectProperty("twist") |> convert<float>

    /// The pointerType read-only property of the PointerEvent interface indicates the device type (mouse, pen, or touch) that caused a given pointer event.
    member x.PointerType =
        match (unbox<string>(r.GetObjectProperty "pointerType")).ToLower().Trim() with
        | "mouse" -> PointerType.Mouse
        | "pen" -> PointerType.Pen
        | "touch" -> PointerType.Touch
        | name -> PointerType.Other name

    /// The isPrimary read-only property of the PointerEvent interface indicates whether or not the pointer device that created the event is the primary pointer. It returns true if the pointer that caused the event to be fired is the primary device and returns false otherwise.
    member x.IsPrimary = r.GetObjectProperty("isPrimary") |> convert<bool>

    /// The getCoalescedEvents() method of the PointerEvent interface returns a sequence of all PointerEvent instances that were coalesced into the dispatched pointermove event.
    member x.GetCoalescedEvents() =
        let o = r.Invoke("getCoalescedEvents") :?> JSObject
        if isNull o then [||]
        else Array.init (convert (o.GetObjectProperty "length")) (fun i -> o.GetObjectProperty(string i) |> convert<PointerEvent>)

    new (o : JsObj) =
        PointerEvent o.Reference

    new (typ : string) =
        PointerEvent(newObj "PointerEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        PointerEvent(newObj "PointerEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        PointerEvent(newObj "PointerEvent" [| typ :> obj; createObj info |])

/// The WheelEvent interface represents events that occur due to the user moving a mouse wheel or similar input device.
[<AllowNullLiteral>]
type WheelEvent(r : JSObject) =
    inherit MouseEvent(r)
     
    /// The WheelEvent.deltaX read-only property is a double representing the horizontal scroll amount in the WheelEvent.deltaMode unit.
    member x.DeltaX = r.GetObjectProperty("deltaX") |> convert<float>

    /// The WheelEvent.deltaY read-only property is a double representing the vertical scroll amount in the WheelEvent.deltaMode unit.
    member x.DeltaY = r.GetObjectProperty("deltaY") |> convert<float>
    
    /// The WheelEvent.deltaZ read-only property is a double representing the scroll amount along the z-axis, in the WheelEvent.deltaMode unit.
    member x.DeltaZ = r.GetObjectProperty("deltaZ") |> convert<float>

    /// The WheelEvent.deltaMode read-only property returns an unsigned long representing the unit of the delta values scroll amount. Permitted values are:
    member x.DeltaMode =
        match convert<int>(r.GetObjectProperty "deltaMode") with
        | 0 -> WheelDeltaMode.Pixel
        | 1 -> WheelDeltaMode.Line
        | 2 -> WheelDeltaMode.Page
        | _ -> WheelDeltaMode.Unknown

    new (o : JsObj) =
        WheelEvent o.Reference

    new (typ : string) =
        WheelEvent(newObj "WheelEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        WheelEvent(newObj "WheelEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        WheelEvent(newObj "WheelEvent" [| typ :> obj; createObj info |])

/// A CloseEvent is sent to clients using WebSockets when the connection is closed. This is delivered to the listener indicated by the WebSocket object's onclose attribute.
[<AllowNullLiteral>]
type CloseEvent(r : JSObject) =
    inherit Event(r)
    
    /// Returns an unsigned short containing the close code sent by the server. The following values are permitted status codes. The following definitions are sourced from the IANA website [Ref]. Note that the 1xxx codes are only WebSocket-internal and not for the same meaning by the transported data (like when the application-layer protocol is invalid). The only permitted codes to be specified in Firefox are 1000 and 3000 to 4999
    member x.Code =
        r.GetObjectProperty "code" |> convert<int> |> unbox<CloseReason>

    /// Returns a DOMString indicating the reason the server closed the connection. This is specific to the particular server and sub-protocol.
    member x.Reason =
        r.GetObjectProperty "reason" |> convert<string>

    /// Returns a Boolean that Indicates whether or not the connection was cleanly closed.
    member x.WasClean =
        r.GetObjectProperty "wasClean" |> convert<bool>

    new (o : JsObj) =
        CloseEvent o.Reference

    new (typ : string) =
        CloseEvent(newObj "CloseEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        CloseEvent(newObj "CloseEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        CloseEvent(newObj "CloseEvent" [| typ :> obj; createObj info |])

/// The DOM CompositionEvent represents events that occur due to the user indirectly entering text.
[<AllowNullLiteral>]
type CompositionEvent(r : JSObject) =
    inherit Event(r)

    /// The data read-only property of the CompositionEvent interface returns the characters generated by the input method that raised the event; its exact nature varies depending on the type of event that generated the CompositionEvent object.
    member x.Data = r.GetObjectProperty "data" |> convert<string>

    new (o : JsObj) =
        CompositionEvent o.Reference

    new (typ : string) =
        CompositionEvent(newObj "CompositionEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        CompositionEvent(newObj "CompositionEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        CompositionEvent(newObj "CompositionEvent" [| typ :> obj; createObj info |])

/// The CustomEvent interface represents events initialized by an application for any purpose.
[<AllowNullLiteral>]
type CustomEvent(r : JSObject) =
    inherit Event(r)
    
    /// The detail readonly property of the CustomEvent interface returns any data passed when initializing the event.
    member x.Detail = r.GetObjectProperty("detail") |> net

    new (o : JsObj) =
        CustomEvent o.Reference

    new (typ : string) =
        CustomEvent(newObj "CustomEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        CustomEvent(newObj "CustomEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        CustomEvent(newObj "CustomEvent" [| typ :> obj; createObj info |])

/// The DeviceLightEvent provides web developers with information from photo sensors or similiar detectors about ambient light levels near the device. For example this may be useful to adjust the screen's brightness based on the current ambient light level in order to save energy or provide better readability.
[<AllowNullLiteral>]
type DeviceLightEvent(r : JSObject) =
    inherit Event(r)
    
    /// The value property provides the current level of the ambient light.
    member x.Value = r.GetObjectProperty("value") |> convert<float>

    new (o : JsObj) =
        DeviceLightEvent o.Reference

    new (typ : string) =
        DeviceLightEvent(newObj "DeviceLightEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        DeviceLightEvent(newObj "DeviceLightEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        DeviceLightEvent(newObj "DeviceLightEvent" [| typ :> obj; createObj info |])

/// The DeviceMotionEvent provides web developers with information about the speed of changes for the device's position and orientation
[<AllowNullLiteral>]
type DeviceMotionEvent(r : JSObject) =
    inherit Event(r)
    
    /// The acceleration property is an object providing information about acceleration on three axis.
    member x.Acceleration = 
        let o = r.GetObjectProperty("acceleration") |> convert<JsObj>
        Aardvark.Base.V3d(convert<float> o.["x"], convert<float> o.["y"], convert<float> o.["z"])
        
    /// The accelerationIncludingGravity property returns the amount of acceleration recorded by the device, in meters per second squared (m/s2). Unlike DeviceMotionEvent.acceleration which compensates for the influence of gravity, its value is the sum of the acceleration of the device as induced by the user and the acceleration caused by gravity.
    member x.AccelerationIncludingGravity = 
        let o = r.GetObjectProperty("accelerationIncludingGravity") |> convert<JsObj>
        Aardvark.Base.V3d(convert<float> o.["x"], convert<float> o.["y"], convert<float> o.["z"])
        
    /// The accelerationIncludingGravity property returns the amount of acceleration recorded by the device, in meters per second squared (m/s2). Unlike DeviceMotionEvent.acceleration which compensates for the influence of gravity, its value is the sum of the acceleration of the device as induced by the user and the acceleration caused by gravity.
    member x.RotationRate = 
        let o = r.GetObjectProperty("rotationRate") |> convert<JsObj>
        Aardvark.Base.V3d(convert<float> o.["gamma"], convert<float> o.["beta"], convert<float> o.["alpha"])
        
    /// Returns the interval, in milliseconds, at which data is obtained from the underlaying hardware. You can use this to determine the granularity of motion events.
    member x.Interval = r.GetObjectProperty("interval") |> convert<float>

    new (o : JsObj) =
        DeviceMotionEvent o.Reference

    new (typ : string) =
        DeviceMotionEvent(newObj "DeviceMotionEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        DeviceMotionEvent(newObj "DeviceMotionEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        DeviceMotionEvent(newObj "DeviceMotionEvent" [| typ :> obj; createObj info |])

/// The DeviceOrientationEvent provides web developers with information from the physical orientation of the device running the web page.
[<AllowNullLiteral>]
type DeviceOrientationEvent(r : JSObject) =
    inherit Event(r)
    
    /// Indicates whether or not the device is providing orientation data absolutely (that is, in reference to the Earth's coordinate frame) or using some arbitrary frame determined by the device. See Orientation and motion data explained for details.
    member x.Absolute = r.GetObjectProperty("absolute") |> convert<bool>
        
    /// Returns the rotation of the device around the Z axis; that is, the number of degrees by which the device is being twisted around the center of the screen. See Orientation and motion data explained for details.
    member x.Alpha = r.GetObjectProperty("alpha") |> convert<float>

    /// Returns the rotation of the device around the X axis; that is, the number of degrees, ranged between -180 and 180,  by which the device is tipped forward or backward. See Orientation and motion data explained for details.
    member x.Beta = r.GetObjectProperty("beta") |> convert<float>

    /// Returns the rotation of the device around the Y axis; that is, the number of degrees, ranged between -90 and 90, by which the device is tilted left or right. See Orientation and motion data explained for details.
    member x.Gamma = r.GetObjectProperty("gamma") |> convert<float>

    /// A number represents the difference between the motion of the device around the z axis of the world system and the direction of the north,  express in degrees with values ranging from 0 to 360.
    member x.CompassHeading = r.GetObjectProperty("webkitCompassHeading") |> convert<float>
    
    /// A number represents the difference between the motion of the device around the z axis of the world system and the direction of the north,  express in degrees with values ranging from 0 to 360.
    member x.CompassAccuracy = r.GetObjectProperty("webkitCompassAccuracy") |> convert<float>

    new (o : JsObj) =
        DeviceOrientationEvent o.Reference

    new (typ : string) =
        DeviceOrientationEvent(newObj "DeviceOrientationEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        DeviceOrientationEvent(newObj "DeviceOrientationEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        DeviceOrientationEvent(newObj "DeviceOrientationEvent" [| typ :> obj; createObj info |])

/// The DragEvent interface is a DOM event that represents a drag and drop interaction. The user initiates a drag by placing a pointer device (such as a mouse) on the touch surface and then dragging the pointer to a new location (such as another DOM element). Applications are free to interpret a drag and drop interaction in an application-specific way.
[<AllowNullLiteral>]
type DragEvent(r : JSObject) =
    inherit MouseEvent(r)
    
    /// The DragEvent.dataTransfer property holds the drag operation's data (as a DataTransfer object).
    member x.DataTransfer = r.GetObjectProperty "dataTransfer" |> convert<DataTransfer>

    new (o : JsObj) =
        DragEvent o.Reference

    new (typ : string) =
        DragEvent(newObj "DragEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        DragEvent(newObj "DragEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        DragEvent(newObj "DragEvent" [| typ :> obj; createObj info |])

/// The ErrorEvent interface represents events providing information related to errors in scripts or in files.
[<AllowNullLiteral>]
type ErrorEvent(r : JSObject) =
    inherit Event(r)
    
    /// Is a DOMString containing a human-readable error message describing the problem.
    member x.Message = r.GetObjectProperty "message" |> convert<string>

    /// Is a DOMString containing the name of the script file in which the error occurred.
    member x.FileName = r.GetObjectProperty "filename" |> convert<string>

    /// Is an integer containing the line number of the script file on which the error occurred.
    member x.Line = r.GetObjectProperty "lineno" |> convert<int>
    
    /// Is an integer containing the column number of the script file on which the error occurred.
    member x.Column = r.GetObjectProperty "colno" |> convert<int>
    
    /// Is a JavaScript Object that is concerned by the event.
    member x.Error = r.GetObjectProperty "error" |> net

    new (o : JsObj) =
        ErrorEvent o.Reference

    new (typ : string) =
        ErrorEvent(newObj "ErrorEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        ErrorEvent(newObj "ErrorEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        ErrorEvent(newObj "ErrorEvent" [| typ :> obj; createObj info |])

/// This is the event type for fetch events dispatched on the service worker global scope. It contains information about the fetch, including the request and how the receiver will treat the response. It provides the event.respondWith() method, which allows us to provide a response to this fetch.
[<AllowNullLiteral>]
type FetchEvent(r : JSObject) =
    inherit ExtendableEvent(r)
    
    /// The clientId read-only property of the FetchEvent interface returns the id of the Client that the current service worker is controlling.
    member x.ClientId = r.GetObjectProperty "clientId" |> convert<string>

    /// The preloadResponse read-only property of the FetchEvent interface returns a Promise that resolves to the navigation preload Response if navigation preload was triggered or undefined otherwise.
    member x.PreloadResponse = r.GetObjectProperty "preloadResponse" |> convert<Task<Response>>
    
    /// The replacesClientId read-only property of the FetchEvent interface is the id of the client that is being replaced during a page navigation.
    member x.ReplacesClientId = r.GetObjectProperty "replacesClientId" |> convert<string>
    
    /// The resultingClientId read-only property of the FetchEvent interface is the id of the client that replaces the previous client during a page navigation.
    member x.ResultingClientId = r.GetObjectProperty "resultingClientId" |> convert<string>

    /// The request read-only property of the FetchEvent interface returns the Request that triggered the event handler.
    member x.Request = r.GetObjectProperty "request" |> convert<Request>

    /// The respondWith() method of FetchEvent prevents the browser's default fetch handling, and allows you to provide a promise for a Response yourself.
    member x.RespondWith(task : Task<Response>) =
        r.Invoke("respondWith", js task) |> ignore
    
    new (o : JsObj) =
        FetchEvent o.Reference

    new (typ : string) =
        FetchEvent(newObj "FetchEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        FetchEvent(newObj "FetchEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        FetchEvent(newObj "FetchEvent" [| typ :> obj; createObj info |])

/// The FocusEvent interface represents focus-related events, including focus, blur, focusin, and focusout.
[<AllowNullLiteral>]
type FocusEvent(r : JSObject) =
    inherit UIEvent(r)
    
    /// The FocusEvent.relatedTarget read-only property is the secondary target.
    member x.RelatedTarget =
        r.GetObjectProperty "relatedTarget" |> convert<EventTarget>

    new (o : JsObj) =
        FocusEvent o.Reference

    new (typ : string) =
        FocusEvent(newObj "FocusEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        FocusEvent(newObj "FocusEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        FocusEvent(newObj "FocusEvent" [| typ :> obj; createObj info |])

/// The GamepadEvent interface of the Gamepad API contains references to gamepads connected to the system, which is what the gamepad events Window.gamepadconnected and Window.gamepaddisconnected are fired in response to.
[<AllowNullLiteral>]
type GamepadEvent(r : JSObject) =
    inherit UIEvent(r)

    /// The GamepadEvent.gamepad property of the GamepadEvent interface returns a Gamepad object, providing access to the associated gamepad data for fired gamepadconnected and gamepaddisconnected events.
    member x.Gamepad = r.GetObjectProperty "gamepad" |> convert<Gamepad>

    new (o : JsObj) =
        GamepadEvent o.Reference

    new (typ : string) =
        GamepadEvent(newObj "GamepadEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        GamepadEvent(newObj "GamepadEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        GamepadEvent(newObj "GamepadEvent" [| typ :> obj; createObj info |])

/// The HashChangeEvent interface represents events that fire when the fragment identifier of the URL has changed.
[<AllowNullLiteral>]
type HashChangeEvent(r : JSObject) =
    inherit Event(r)

    /// The newURL read-only property of the HashChangeEvent interface returns the new URL to which the window is navigating.
    member x.NewUrl = r.GetObjectProperty "newURL" |> convert<string>

    /// The oldURL read-only property of the HashChangeEvent interface returns the previous URL from which the window was navigated.
    member x.OldUrl = r.GetObjectProperty "newURL" |> convert<string>

    new (o : JsObj) =
        HashChangeEvent o.Reference

    new (typ : string) =
        HashChangeEvent(newObj "HashChangeEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        HashChangeEvent(newObj "HashChangeEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        HashChangeEvent(newObj "HashChangeEvent" [| typ :> obj; createObj info |])

/// The IDBVersionChangeEvent interface of the IndexedDB API indicates that the version of the database has changed, as the result of an IDBOpenDBRequest.onupgradeneeded event handler function.
[<AllowNullLiteral>]
type IDBVersionChangeEvent(r : JSObject) =
    inherit Event(r)

    /// The oldVersion read-only property of the IDBVersionChangeEvent interface returns the old version number of the database.
    member x.OldVersion : int64 = r.GetObjectProperty "oldVersion" |> convert<int64>
    
    /// The newVersion read-only property of the IDBVersionChangeEvent interface returns the new version number of the database.
    member x.NewVersion : int64 = r.GetObjectProperty "newVersion" |> convert<int64>


    new (o : JsObj) =
        IDBVersionChangeEvent o.Reference

    new (typ : string) =
        IDBVersionChangeEvent(newObj "IDBVersionChangeEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        IDBVersionChangeEvent(newObj "IDBVersionChangeEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        IDBVersionChangeEvent(newObj "IDBVersionChangeEvent" [| typ :> obj; createObj info |])

/// The InputEvent interface represents an event notifying of editable content change.
[<AllowNullLiteral>]
type InputEvent(r : JSObject) =
    inherit UIEvent(r)

    /// The data read-only property of the InputEvent interface returns a DOMString with the inserted characters. This may be an empty string if the change doesn't insert text (such as when deleting characters, for example).
    member x.Data = r.GetObjectProperty "data" |> convert<string>
    
    /// The dataTransfer read-only property of the InputEvent interface returns a DataTransfer object containing information about richtext or plaintext data being added to or removed from editible content.
    member x.DataTransfer = r.GetObjectProperty "dataTransfer" |> convert<DataTransfer>
    
    /// The inputType read-only property of the InputEvent interface returns the type of change made to editible content. Possible changes include for example inserting, deleting, and formatting text.
    member x.InputType = r.GetObjectProperty "inputType" |> convert<string>

    /// The InputEvent.isComposing read-only property returns a Boolean value indicating if the event is fired after compositionstart and before compositionend.
    member x.IsComposing = r.GetObjectProperty "isComposing" |> convert<bool>

    new (o : JsObj) =
        InputEvent o.Reference

    new (typ : string) =
        InputEvent(newObj "InputEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        InputEvent(newObj "InputEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        InputEvent(newObj "InputEvent" [| typ :> obj; createObj info |])

/// KeyboardEvent objects describe a user interaction with the keyboard; each event describes a single interaction between the user and a key (or combination of a key with modifier keys) on the keyboard. The event type (keydown, keypress, or keyup) identifies what kind of keyboard activity occurred.
[<AllowNullLiteral>]
type KeyboardEvent(r : JSObject) =
    inherit UIEvent(r)

    /// Returns a Boolean that is true if the Alt ( Option or ⌥ on OS X) key was active when the key event was generated.
    member x.Alt = r.GetObjectProperty "altKey" |> convert<bool>
    
    /// Returns a Boolean that is true if the Ctrl key was active when the key event was generated
    member x.Ctrl = r.GetObjectProperty "ctrlKey" |> convert<bool>
    
    /// Returns a Boolean that is true if the Meta key (on Mac keyboards, the ⌘ Command key; on Windows keyboards, the Windows key (⊞)) was active when the key event was generated.
    member x.Meta = r.GetObjectProperty "metaKey" |> convert<bool>
        
    /// Returns a Boolean that is true if the Shift key was active when the key event was generated.
    member x.Shift = r.GetObjectProperty "shiftKey" |> convert<bool>

    /// Returns a DOMString with the code value of the physical key represented by the event.
    member x.Code = r.GetObjectProperty "code" |> convert<string>
    
    /// Returns a DOMString with the code value of the physical key represented by the event.
    member x.Key = r.GetObjectProperty "key" |> convert<string>
    
    /// Returns a Boolean that is true if the key is being held down such that it is automatically repeating.
    member x.Repeat = r.GetObjectProperty "repeat" |> convert<bool>
    
    /// Returns a Number representing the location of the key on the keyboard or other input device. A list of the constants identifying the locations is shown above in Keyboard locations.
    member x.Location = r.GetObjectProperty "location" |> convert<int> |> unbox<KeyboardLocation>
    
    /// Returns a Boolean indicating if a modifier key such as Alt, Shift, Ctrl, or Meta, was pressed when the event was create
    member x.GetModifierState(key : string) =
        r.Invoke("getModifierState", key) |> convert<bool>

    new (o : JsObj) =
        KeyboardEvent o.Reference

    new (typ : string) =
        KeyboardEvent(newObj "KeyboardEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        KeyboardEvent(newObj "KeyboardEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        KeyboardEvent(newObj "KeyboardEvent" [| typ :> obj; createObj info |])

/// The MessageEvent interface represents a message received by a target object.
[<AllowNullLiteral>]
type MessageEvent(r : JSObject) =
    inherit Event(r)

    /// The data sent by the message emitter.
    member x.Data = r.GetObjectProperty "data" |> net

    /// A USVString representing the origin of the message emitter.
    member x.Origin = r.GetObjectProperty "origin" |> convert<string>

    /// A DOMString representing a unique ID for the event.
    member x.LastEventId = r.GetObjectProperty "lastEventId" |> convert<string>

    // TODO: source
    //       ports

    new (o : JsObj) =
        MessageEvent o.Reference

    new (typ : string) =
        MessageEvent(newObj "MessageEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        MessageEvent(newObj "MessageEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        MessageEvent(newObj "MessageEvent" [| typ :> obj; createObj info |])

/// The PageTransitionEvent event object is available inside handler functions for the pageshow and pagehide events, fired when a document is being loaded or unloaded.
[<AllowNullLiteral>]
type PageTransitionEvent(r : JSObject) =
    inherit Event(r)
    
    /// The persisted read-only property indicates if a webpage is loading from a cache.
    member x.Persisted = r.GetObjectProperty "persisted" |> convert<bool>

    new (o : JsObj) =
        PageTransitionEvent o.Reference

    new (typ : string) =
        PageTransitionEvent(newObj "PageTransitionEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        PageTransitionEvent(newObj "PageTransitionEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        PageTransitionEvent(newObj "PageTransitionEvent" [| typ :> obj; createObj info |])

/// The ProgressEvent interface represents events measuring progress of an underlying process, like an HTTP request (for an XMLHttpRequest, or the loading of the underlying resource of an <img>, <audio>, <video>, <style> or <link>).
[<AllowNullLiteral>]
type ProgressEvent(r : JSObject) =
    inherit Event(r)
    
    /// Is a Boolean flag indicating if the total work to be done, and the amount of work already done, by the underlying process is calculable. In other words, it tells if the progress is measurable or not.
    member x.LengthComputable = r.GetObjectProperty "lengthComputable" |> convert<bool>

    /// Is an unsigned long long representing the amount of work already performed by the underlying process. The ratio of work done can be calculated with the property and ProgressEvent.total. When downloading a resource using HTTP, this only represent the part of the content itself, not headers and other overhead.
    member x.Loaded = r.GetObjectProperty "loaded" |> convert<uint64>

    /// Is an unsigned long long representing the total amount of work that the underlying process is in the progress of performing. When downloading a resource using HTTP, this only represent the content itself, not headers and other overhead.
    member x.Total = r.GetObjectProperty "total" |> convert<uint64>

    new (o : JsObj) =
        ProgressEvent o.Reference

    new (typ : string) =
        ProgressEvent(newObj "ProgressEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        ProgressEvent(newObj "ProgressEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        ProgressEvent(newObj "ProgressEvent" [| typ :> obj; createObj info |])

/// A StorageEvent is sent to a window when a storage area it has access to is changed within the context of another document.
[<AllowNullLiteral>]
type StorageEvent(r : JSObject) =
    inherit Event(r)
    
    /// Represents the key changed. The key attribute is null when the change is caused by the storage clear() method. Read only.
    member x.Key = r.GetObjectProperty "key" |> convert<string>

    /// The new value of the key. The newValue is null when the change has been invoked by storage clear() method or the key has been removed from the storage. Read only.
    member x.NewValue = r.GetObjectProperty "newValue" |> net
    
    /// The original value of the key. The oldValue is null when the key has been newly added and therefore doesn't have any previous value. Read only.
    member x.OldValue = r.GetObjectProperty "oldValue" |> net

    /// Represents the Storage object that was affected. Read only.
    member x.StorageArea = r.GetObjectProperty "oldValue" |> convert<JsObj>

    /// The URL of the document whose key changed. Read only.
    member x.Url = r.GetObjectProperty "url" |> convert<string>

    new (o : JsObj) =
        StorageEvent o.Reference

    new (typ : string) =
        StorageEvent(newObj "StorageEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        StorageEvent(newObj "StorageEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        StorageEvent(newObj "StorageEvent" [| typ :> obj; createObj info |])

/// The SVGEvent interface represents the event object for most SVG-related events.
[<AllowNullLiteral>]
type SVGEvent(r : JSObject) =
    inherit Event(r)

    new (o : JsObj) =
        SVGEvent o.Reference

    new (typ : string) =
        SVGEvent(newObj "SVGEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        SVGEvent(newObj "SVGEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        SVGEvent(newObj "SVGEvent" [| typ :> obj; createObj info |])

/// The TimeEvent interface, a part of SVG SMIL animation, provides specific contextual information associated with Time events.
[<AllowNullLiteral>]
type TimeEvent(r : JSObject) =
    inherit Event(r)

    /// Is a long that specifies some detail information about the Event, depending on the type of the event. For this event type, indicates the repeat number for the animation.
    member x.Detail = r.GetObjectProperty "detail" |> convert<int>

    /// Is a WindowProxy that identifies the Window from which the event was generated.
    member x.View = r.GetObjectProperty "view" |> convert<JsObj>

    new (o : JsObj) =
        TimeEvent o.Reference

    new (typ : string) =
        TimeEvent(newObj "TimeEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        TimeEvent(newObj "TimeEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        TimeEvent(newObj "TimeEvent" [| typ :> obj; createObj info |])

/// The TouchEvent interface represents an UIEvent which is sent when the state of contacts with a touch-sensitive surface changes. This surface can be a touch screen or trackpad, for example. The event can describe one or more points of contact with the screen and includes support for detecting movement, addition and removal of contact points, and so forth.
[<AllowNullLiteral>]
type TouchEvent(r : JSObject) =
    inherit UIEvent(r)

    /// Returns a Boolean that is true if the Alt ( Option or ⌥ on OS X) key was active when the key event was generated.
    member x.Alt = r.GetObjectProperty "altKey" |> convert<bool>
    
    /// Returns a Boolean that is true if the Ctrl key was active when the key event was generated
    member x.Ctrl = r.GetObjectProperty "ctrlKey" |> convert<bool>
    
    /// Returns a Boolean that is true if the Meta key (on Mac keyboards, the ⌘ Command key; on Windows keyboards, the Windows key (⊞)) was active when the key event was generated.
    member x.Meta = r.GetObjectProperty "metaKey" |> convert<bool>
        
    /// Returns a Boolean that is true if the Shift key was active when the key event was generated.
    member x.Shift = r.GetObjectProperty "shiftKey" |> convert<bool>

    /// The changedTouches read-only property is a TouchList whose touch points (Touch objects) varies depending on the event type.
    member x.ChangedTouches = r.GetObjectProperty "changedTouches" |> convert<TouchList>

    /// The targetTouches read-only property is a TouchList listing all the Touch objects for touch points that are still in contact with the touch surface and whose touchstart event occurred inside the same target element as the current target element.
    member x.TargetTouches = r.GetObjectProperty "targetTouches" |> convert<TouchList>
    
    /// touches is a read-only TouchList listing all the Touch objects for touch points that are currently in contact with the touch surface, regardless of whether or not they've changed or what their target element was at touchstart time.
    member x.Touches = r.GetObjectProperty "touches" |> convert<TouchList>

    /// Change in rotation (in degrees) since the event's beginning. Positive values indicate clockwise rotation; negative values indicate anticlockwise rotation. Initial value: 0.0
    member x.Rotation = r.GetObjectProperty "rotation" |> convert<float>

    /// Distance between two digits since the event's beginning. Expressed as a floating-point multiple of the initial distance between the digits at the beginning of the event. Values below 1.0 indicate an inward pinch (zoom out). Values above 1.0 indicate an outward unpinch (zoom in). Initial value: 1.0
    member x.Scale = r.GetObjectProperty "scale" |> convert<float>

    new (o : JsObj) =
        TouchEvent o.Reference

    new (typ : string) =
        TouchEvent(newObj "TouchEvent" [| typ :> obj|])
        
    new (typ : string, info : obj) =    
        TouchEvent(newObj "TouchEvent" [| typ :> obj; js info |])

    new (typ : string, info : list<string * obj>) =
        TouchEvent(newObj "TouchEvent" [| typ :> obj; createObj info |])


/// EventTarget is a DOM interface implemented by objects that can receive events and may have listeners for them.
[<AllowNullLiteral>]
type EventTarget(r : JSObject) =
    inherit JsObj(r)

    /// The EventTarget method addEventListener() sets up a function that will be called whenever the specified event is delivered to the target. Common targets are Element, Document, and Window, but the target may be any object that supports events (such as XMLHttpRequest).
    member x.AddEventListener(name : string, listener : Event -> unit, useCapture : bool) =
        use l = new JSObject()
        l.SetObjectProperty("handleEvent", System.Action<JSObject>(fun e -> Event(e) |> listener))
        r.Invoke("addEventListener", name, l, useCapture) |> ignore
        
    /// The EventTarget.removeEventListener() method removes from the EventTarget an event listener previously registered with EventTarget.addEventListener(). The event listener to be removed is identified using a combination of the event type, the event listener function itself, and various optional options that may affect the matching process; see Matching event listeners for removal
    member x.AddEventListener(name : string, listener : Event -> unit) =
        x.AddEventListener(name, listener, false)

    /// Dispatches an Event at the specified EventTarget, (synchronously) invoking the affected EventListeners in the appropriate order. The normal event processing rules (including the capturing and optional bubbling phase) also apply to events dispatched manually with dispatchEvent().
    member x.Dispatch(evt : Event) : bool =
        r.Invoke("dispatchEvent", evt.Reference) |> unbox<bool>

    /// The EventTarget method SubscribeEventListener() sets up a function that will be called whenever the specified event is delivered to the target. Common targets are Element, Document, and Window, but the target may be any object that supports events (such as XMLHttpRequest).
    member x.SubscribeEventListener(name : string, listener : Event -> unit, useCapture : bool) =
        let l = new JSObject()
        l.SetObjectProperty("handleEvent", System.Action<JSObject>(fun e -> Event(e) |> listener))
        r.Invoke("addEventListener", name, l, useCapture) |> ignore

        { new IDisposable with
            member x.Dispose() = 
                r.Invoke("removeEventListener", name, l, useCapture) |> ignore
                l.Dispose()

        }
        
    /// The EventTarget method SubscribeEventListener() sets up a function that will be called whenever the specified event is delivered to the target. Common targets are Element, Document, and Window, but the target may be any object that supports events (such as XMLHttpRequest).
    member x.SubscribeEventListener(name : string, listener : Event -> unit) =
        x.SubscribeEventListener(name, listener, false)
        
    new (o : JsObj) =
        EventTarget o.Reference

/// The DOM Node interface is a key base class upon which many other DOM API objects are based, thus letting those object types to be used similarly and often interchangeably. Key among the interfaces which inherit the features of Node are Document and Element. However, all of the following also inherit methods and properties from Node: Attr, CharacterData (which Text, Comment, and CDATASection are all based on), ProcessingInstruction, DocumentFragment, DocumentType, Notation, Entity, and EntityReference.
[<AllowNullLiteral>]
type Node(r : JSObject) =
    inherit EventTarget(r)

    /// Returns a DOMString representing the base URL of the document containing the Node.
    member x.BaseURI : string =
        r.GetObjectProperty("baseURI") |> convert<string>

    /// Returns a live NodeList containing all the children of this node. NodeList being live means that if the children of the Node change, the NodeList object is automatically updated.
    member x.ChildNodes : NodeList =
        r.GetObjectProperty("childNodes") |> convert<NodeList> 

    /// Returns a Node representing the first direct child node of the node, or null if the node has no child.
    member x.FirstChild = r.GetObjectProperty("firstChild") |> convert<Node>

    /// A boolean indicating whether or not the Node is connected (directly or indirectly) to the context object, e.g. the Document object in the case of the normal DOM, or the ShadowRoot in the case of a shadow DOM.
    member x.IsConnected : bool = r.GetObjectProperty("isConnected") |> convert

    /// Returns a Node representing the last direct child node of the node, or null if the node has no child.
    member x.LastChild = r.GetObjectProperty("lastChild") |> convert<Node>
        
    /// Returns a Node representing the next node in the tree, or null if there isn't such node.
    member x.NextSibling = r.GetObjectProperty("nextSibling") |> convert<Node>
        
    /// Returns a DOMString containing the name of the Node. The structure of the name will differ with the node type. E.g. An HTMLElement will contain the name of the corresponding tag, like 'audio' for an HTMLAudioElement, a Text node will have the '#text' string, or a Document node will have the '#document' string.
    member x.NodeName : string = r.GetObjectProperty("nodeName") |> convert

    /// Returns an unsigned short representing the type of the node. 
    member x.NodeType : NodeType =
        r.GetObjectProperty("nodeType") |> unbox<int> |> unbox<NodeType>
        
    /// Returns / Sets the value of the current node.
    member x.NodeValue
        with get() = r.GetObjectProperty("nodeValue") |> net
        and set (v : obj) = r.SetObjectProperty("nodeValue", js v)

    /// Returns a Node that is the parent of this node. If there is no such node, like if this node is the top of the tree or if doesn't participate in a tree, this property returns null.
    member x.ParentNode = r.GetObjectProperty("parentNode") |> convert<Node> 
    
    /// Returns an Element that is the parent of this node. If the node has no parent, or if that parent is not an Element, this property returns null.
    member x.ParentElement = r.GetObjectProperty("parentElement") |> convert<Element> 

    /// Returns a Node representing the previous node in the tree, or null if there isn't such node.
    member x.PreviousSibling = r.GetObjectProperty("previousSibling") |> convert<Node>

    /// Returns / Sets the textual content of an element and all its descendants.
    member x.TextContent
        with get() = r.GetObjectProperty("textContent") |> convert<string>
        and set (v : string) = r.SetObjectProperty("textContent", js v)

    /// Adds the specified childNode argument as the last child to the current node. If the argument referenced an existing node on the DOM tree, the node will be detached from its current position and attached at the new position.
    member x.AppendChild(node : Node) =
        r.Invoke("appendChild", js node) |> ignore

    /// Clone a Node, and optionally, all of its contents. By default, it clones the content of the node.
    member x.CloneNode() =
        r.Invoke("cloneNode") |> convert<Node>

    /// Compares the position of the current node against another node in any other document.
    member x.CompareDocumentPosition(other : Node) =
        r.Invoke("compareDocumentPosition", other.Reference) |> unbox<int> |> unbox<DocumentPosition>
        
    /// Returns a Boolean value indicating whether or not a node is a descendant of the calling node.
    member x.Contains(other : Node) =
        r.Invoke("contains", other.Reference) |> unbox<bool>
        
    /// Returns the context object's root which optionally includes the shadow root if it is available. 
    member x.RootNode =
        r.Invoke("getRootNode") |> unbox<JSObject> |> Node

    /// Returns a Boolean indicating whether or not the element has any child nodes.
    member x.HasChildNodes =
        r.Invoke("hasChildNodes") |> unbox<bool>
        
    /// Accepts a namespace URI as an argument and returns a Boolean with a value of true if the namespace is the default namespace on the given node or false if not.
    member x.IsDefaultNamespace =
        r.Invoke("isDefaultNamespace") |> unbox<bool>
        
    /// Returns a Boolean which indicates whether or not two nodes are of the same type and all their defining data points match.
    member x.IsEqualNode(other : Node) =
        r.Invoke("isEqualNode", other.Reference) |> unbox<bool>
        
    /// Returns a Boolean value indicating whether or not the two nodes are the same (that is, they reference the same object)
    member x.IsSameNode(other : Node) =
        r.Invoke("isSameNode", other.Reference) |> unbox<bool>
        
    /// Returns a DOMString containing the prefix for a given namespace URI, if present, and null if not. When multiple prefixes are possible, the result is implementation-dependent.
    member x.LookupPrefix =
        r.Invoke("lookupPrefix") |> unbox<string>
        
    /// Accepts a prefix and returns the namespace URI associated with it on the given node if found (and null if not). Supplying null for the prefix will return the default namespace.
    member x.LookupNamespaceURI =
        r.Invoke("lookupNamespaceURI") |> unbox<string>
        
    /// Clean up all the text nodes under this element (merge adjacent, remove empty).
    member x.Normalize() =
        r.Invoke("normalize") |> ignore

    /// Removes a child node from the current element, which must be a child of the current node.
    member x.RemoveChild(node : Node) =
        r.Invoke("removeChild", node.Reference) |> ignore
        
    /// Replaces one child Node of the current one with the second one given in parameter.
    member x.ReplaceChild(newNode : Node, oldNode : Node) =
        r.Invoke("replaceChild", newNode.Reference, oldNode.Reference) |> ignore

    /// Inserts a Node before the reference node as a child of a specified parent node
    member x.InsertBefore(newNode : Node, referenceNode : Node) =
        let nn = if isNull newNode then null else newNode.Reference
        let rr = if isNull referenceNode then null else referenceNode.Reference
        r.Invoke("insertBefore", nn, rr) |> ignore
        
    // TODO: OwnerDocument

    new (o : JsObj) =
        Node o.Reference
    
/// Element is the most general base class from which all element objects (i.e. objects that represent elements) in a Document inherit. It only has methods and properties common to all kinds of elements. More specific classes inherit from Element. For example, the HTMLElement interface is the base interface for HTML elements, while the SVGElement interface is the basis for all SVG elements. Most functionality is specified further down the class hierarchy.
[<AllowNullLiteral>]
type Element(r : JSObject) =
    inherit Node(r)

    /// Returns a NamedNodeMap object containing the assigned attributes of the corresponding HTML element.
    member x.Attributes =
        r.GetObjectProperty("attributes") |> convert<NamedNodeMap>

    /// Returns a DOMTokenList containing the list of class attributes.
    member x.ClassList =
        r.GetObjectProperty("classList") |> convert<TokenList>
        
    /// Is a DOMString representing the class of the element.
    member x.ClassName
        with get() = r.GetObjectProperty "className" |> unbox<string>
        and set (v : string) = r.SetObjectProperty("className", v)

    /// Returns a Number representing the inner height of the element.
    member x.ClientHeight = r.GetObjectProperty "clientHeight" |> convert<float>

    /// Returns a Number representing the width of the left border of the element.
    member x.ClientLeft = r.GetObjectProperty "clientLeft" |> convert<float>

    /// Returns a Number representing the width of the top border of the element.
    member x.ClientTop = r.GetObjectProperty "clientTop" |> convert<float>

    /// Returns a Number representing the inner width of the element.
    member x.ClientWidth = r.GetObjectProperty "clientWidth" |> convert<float>

    /// Returns a DOMString containing the label exposed to accessibility.
    member x.ComputedName = r.GetObjectProperty "computedName" |> convert<string>
    
    /// Returns a DOMString containing the ARIA role that has been applied to a particular element.
    member x.ComputedRole = r.GetObjectProperty "computedRole" |> convert<string>
    
    /// Is a DOMString representing the id of the element.
    member x.Id     
        with get() = r.GetObjectProperty "id" |> convert<string>
        and set (v : string) = r.SetObjectProperty("id", v)
        
    /// Is a DOMString representing the markup of the element's content.
    member x.InnerHTML     
        with get() = r.GetObjectProperty "innerHTML" |> convert<string>
        and set (v : string) = r.SetObjectProperty("innerHTML", v)

    /// A DOMString representing the local part of the qualified name of the element.
    member x.LocalName = r.GetObjectProperty "localName" |> convert<string>
    
    /// The namespace URI of the element, or null if it is no namespace.
    member x.NamespaceURI = r.GetObjectProperty "namespaceURI" |> convert<string>
    
    /// Is an Element, the element immediately following the given one in the tree, or null if there's no sibling node.
    member x.NextElementSibling = r.GetObjectProperty "nextElementSibling" |> convert<Element>
    
    /// Is a DOMString representing the markup of the element including its content. When used as a setter, replaces the element with nodes parsed from the given string.
    member x.OuterHTML     
        with get() = r.GetObjectProperty "outerHTML" |> convert<string>
        and set (v : string) = r.SetObjectProperty("outerHTML", v)

    /// Represents the part identifier(s) of the element (i.e. set using the part attribute), returned as a DOMTokenList.
    member x.Part
        with get() = r.GetObjectProperty "part" |> convert<TokenList>
        and set(v : TokenList) = r.SetObjectProperty("part", js v)

    /// A DOMString representing the namespace prefix of the element, or null if no prefix is specified.
    member x.Prefix = r.GetObjectProperty "prefix" |> convert<string>
    
    /// Is an Element, the element immediately following the given one in the tree, or null if there's no sibling node.
    member x.PreviousElementSibling = r.GetObjectProperty "previousElementSibling" |> convert<Element>
    
    /// Returns a Number representing the scroll view height of an element.
    member x.ScrollHeight = r.GetObjectProperty "scrollHeight" |> convert<float>
    
    /// Is a Number representing the left scroll offset of the element.
    member x.ScrollLeft = r.GetObjectProperty "scrollLeft" |> convert<float>
    
    /// Returns a Number representing the maximum left scroll offset possible for the element.
    member x.ScrollLeftMax = r.GetObjectProperty "scrollLeftMax" |> convert<float>
    
    /// A Number representing number of pixels the top of the document is scrolled vertically.
    member x.ScrollTop = r.GetObjectProperty "scrollTop" |> convert<float>
    
    /// Returns a Number representing the maximum top scroll offset possible for the element.
    member x.ScrollTopMax = r.GetObjectProperty "scrollTopMax" |> convert<float>
    
    /// Returns a Number representing the scroll view width of the element.
    member x.ScrollWidth = r.GetObjectProperty "scrollWidth" |> convert<float>

    /// Is a Boolean indicating if the element can receive input focus via the tab key.
    member x.TabStop = r.GetObjectProperty "tabStop" |> convert<bool>

    /// Returns a String with the name of the tag for the given element.
    member x.TagName = r.GetObjectProperty "tagName" |> convert<string>

    /// The closest() method traverses the Element and its parents (heading toward the document root) until it finds a node that matches the provided selector string. Will return itself or the matching ancestor. If no such element exists, it returns null.
    member x.Closest(selectors : string) =
        r.Invoke("closest", selectors) |> convert<Element>

    /// The computedStyleMap() method of the Element interface returns a CSSStyleDeclaration.
    member x.ComputedStyleMap() =
        r.Invoke("computedStyleMap") |> convert<CSSStyleDeclaration>

    /// The getAttribute() method of the Element interface returns the value of a specified attribute on the element. If the given attribute does not exist, the value returned will either be null or "" (the empty string); see Non-existing attributes for details.
    member x.GetAttribute(attributeName : string) =
        r.Invoke("getAttribute", attributeName) |> net
        
    /// The getAttributeNames() method of the Element interface returns the attribute names of the element as an Array of strings. If the element has no attributes it returns an empty array.
    member x.GetAttributeNames() =
        let o = r.Invoke("getAttributeNames")
        match o with
        | null -> 
            [||]
        | :? JSObject as o -> 
            let l = o.GetObjectProperty "length" |> convert<int>
            Array.init l (fun i -> o.GetObjectProperty(string i) |> convert<string>)
        | _ ->
            [||]
            
    /// The getAttributeNS() method of the Element interface returns the string value of the attribute with the specified namespace and name. If the named attribute does not exist, the value returned will either be null or "" (the empty string); see Notes for details.
    member x.GetAttribute(ns : string, attributeName : string) =
        r.Invoke("getAttributeNS", ns, attributeName) |> net
        
    /// The Element.getBoundingClientRect() method returns the size of an element and its position relative to the viewport.
    member x.GetBoundingClientRect() =
        let o = r.Invoke("getBoundingClientRect") |> convert<JsObj>
        let x = convert<float> o.["x"]
        let y = convert<float> o.["y"]
        let w = convert<float> o.["width"]
        let h = convert<float> o.["height"]
        Aardvark.Base.Box2d.FromMinAndSize(x, y, w, h)

    /// The Element method getElementsByClassName() returns a live HTMLCollection which contains every descendant element which has the specified class name or names.
    member x.GetElementsByClassName(name : string) =
        r.Invoke("getElementsByClassName", name) |> convert<HTMLCollection>

    /// The Element.getElementsByTagName() method returns a live HTMLCollection of elements with the given tag name. All descendants of the specified element are searched, but not the element itself. The returned list is live, which means it updates itself with the DOM tree automatically. Therefore, there is no need to call Element.getElementsByTagName() with the same element and arguments repeatedly if the DOM changes in between calls.
    member x.GetElementsByTagName(name : string) =
        r.Invoke("getElementsByTagName", name) |> convert<HTMLCollection>
        
    /// The Element method getElementsByClassName() returns a live HTMLCollection which contains every descendant element which has the specified class name or names.
    member x.GetElementsByTagName(ns : string, name : string) =
        r.Invoke("getElementsByTagNameNS", ns, name) |> convert<HTMLCollection>

    /// Returns a Boolean indicating if the element has the specified attribute or not.
    member x.HasAttribute(name : string) = r.Invoke("hasAttribute", name) |> convert<bool>

    /// Returns a Boolean indicating if the element has the specified attribute or not.
    member x.HasAttribute(ns : string, name : string) = r.Invoke("hasAttributeNS", ns, name) |> convert<bool>

    /// The hasAttributes() method of the Element interface returns a Boolean indicating whether the current element has any attributes or not.
    member x.HasAttributes() = r.Invoke("hasAttributes") |> convert<bool>

    /// The hasPointerCapture() method of the Element interface sets whether the element on which it is invoked has pointer capture for the pointer identified by the given pointer ID.
    member x.HasPointerCapture(id : int) = r.Invoke("hasPointerCapture", id) |> convert<bool>
    
    /// The insertAdjacentElement() method of the Element interface inserts a given element node at a given position relative to the element it is invoked upon.
    member x.InsertAdjacentElement(position : InsertMode, element : Element) = 
        let position =
            match position with
            | InsertMode.BeforeBegin -> "beforebegin"
            | InsertMode.AfterBegin -> "afterbegin"
            | InsertMode.BeforeEnd -> "beforeend"
            | InsertMode.AfterEnd -> "afterend"
    
        r.Invoke("insertAdjacentElement", position, js element) |> ignore
    
    /// The insertAdjacentHTML() method of the Element interface parses the specified text as HTML or XML and inserts the resulting nodes into the DOM tree at a specified position. It does not reparse the element it is being used on, and thus it does not corrupt the existing elements inside that element. This avoids the extra step of serialization, making it much faster than direct innerHTML manipulation.
    member x.InsertAdjacentHTML(position : InsertMode, text : string) = 
        let position =
            match position with
            | InsertMode.BeforeBegin -> "beforebegin"
            | InsertMode.AfterBegin -> "afterbegin"
            | InsertMode.BeforeEnd -> "beforeend"
            | InsertMode.AfterEnd -> "afterend"
    
        r.Invoke("insertAdjacentHTML", position, text) |> ignore

    /// The insertAdjacentText() method of the Element interface inserts a given text node at a given position relative to the element it is invoked upon.
    member x.InsertAdjacentText(position : InsertMode, text : string) = 
        let position =
            match position with
            | InsertMode.BeforeBegin -> "beforebegin"
            | InsertMode.AfterBegin -> "afterbegin"
            | InsertMode.BeforeEnd -> "beforeend"
            | InsertMode.AfterEnd -> "afterend"
    
        r.Invoke("insertAdjacentText", position, text) |> ignore

    /// The matches() method checks to see if the Element would be selected by the provided selectorString -- in other words -- checks if the element "is" the selector.
    member x.Matches(selector : string) =
        r.Invoke("matches", selector) |> convert<bool>

    /// The querySelector() method of the Element interface returns the first element that is a descendant of the element on which it is invoked that matches the specified group of selectors.
    member x.QuerySelector(selector : string) =
        r.Invoke("querySelector", selector) |> convert<Element>
        
    /// The Element method querySelectorAll() returns a static (not live) NodeList representing a list of elements matching the specified group of selectors which are descendants of the element on which the method was called.
    member x.QuerySelectorAll(selector : string) =
        r.Invoke("querySelectorAll", selector) |> convert<NodeList>

    /// The releasePointerCapture() method of the Element interface releases (stops) pointer capture that was previously set for a specific (PointerEvent) pointer.
    member x.ReleasePointerCapture(id : int) =
        r.Invoke("releasePointerCapture", id) |> ignore

    /// Removes the element from the children list of its parent.
    member x.Remove() =
        r.Invoke("remove") |> ignore

    /// The Element method removeAttribute() removes the attribute with the specified name from the element.
    member x.RemoveAttribute(name : string) =
        r.Invoke("removeAttribute", name) |> ignore
        
    /// The Element method removeAttribute() removes the attribute with the specified name from the element.
    member x.RemoveAttribute(ns : string, name : string) =
        r.Invoke("removeAttributeNS", ns, name) |> ignore

    /// The Element.requestFullscreen() method issues an asynchronous request to make the element be displayed in full-screen mode.
    member x.RequestFullscreen(?options : obj) =
        match options with
        | Some o -> r.Invoke("requestFullscreen", js o) |> convert<Task>
        | None -> r.Invoke("requestFullscreen") |> convert<Task>
        
    /// The Element.requestPointerLock() method lets you asynchronously ask for the pointer to be locked on the given element.
    member x.RequestPointerLock() =
        r.Invoke("requestPointerLock") |> ignore
        
    /// The scroll() method of the Element interface scrolls the element to a particular set of coordinates inside a given element.
    member __.Scroll(pos : Aardvark.Base.V2d, ?behaviour : ScrollBehaviour) =
        match behaviour with
        | None -> r.Invoke("scroll", pos.X, pos.Y) |> ignore
        | Some b ->
            r.Invoke(
                "scroll",
                createObj [
                    "top", pos.Y :> obj
                    "left", pos.X :> obj
                    "behavior", (match b with | ScrollBehaviour.Auto -> "auto" | ScrollBehaviour.Smooth -> "smooth") :> obj
                ]
            ) |> ignore

    /// The Element interface's scrollIntoView() method scrolls the element's parent container such that the element on which scrollIntoView() is called is visible to the user
    member x.ScrollIntoView(?alignToTop : bool) =
        match alignToTop with
        | Some a -> r.Invoke("scrollIntoView", a) |> ignore
        | None -> r.Invoke("scrollIntoView") |> ignore
        
    /// The Element interface's scrollIntoView() method scrolls the element's parent container such that the element on which scrollIntoView() is called is visible to the user
    member x.ScrollIntoView(?behavior : ScrollBehaviour, ?block : string, ?inl : string) =
        r.Invoke("scrollIntoView", 
            createObj [
                match behavior with
                | Some ScrollBehaviour.Auto -> "behaviour", "auto" :> obj
                | Some ScrollBehaviour.Smooth -> "behaviour", "smooth" :> obj
                | None -> ()

                match block with
                | Some b -> "block", b :> obj
                | None -> ()
                
                match inl with
                | Some b -> "inline", b :> obj
                | None -> ()
            ]
        ) |> ignore
        
    /// The scrollTo() method of the Element interface scrolls to a particular set of coordinates inside a given element.
    member __.ScrollTo(pos : Aardvark.Base.V2d, ?behaviour : ScrollBehaviour) =
        match behaviour with
        | None -> r.Invoke("scrollTo", pos.X, pos.Y) |> ignore
        | Some b ->
            r.Invoke(
                "scrollTo",
                createObj [
                    "top", pos.Y :> obj
                    "left", pos.X :> obj
                    "behavior", (match b with | ScrollBehaviour.Auto -> "auto" | ScrollBehaviour.Smooth -> "smooth") :> obj
                ]
            ) |> ignore

    /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
    member x.SetAttriubte(name : string, value : obj) =
        r.Invoke("setAttribute", name, js value) |> ignore
        
    /// Sets the value of an attribute on the specified element. If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
    member x.SetAttriubte(ns : string, name : string, value : obj) =
        r.Invoke("setAttributeNS", ns, name, js value) |> ignore

    /// Call this method during the handling of a mousedown event to retarget all mouse events to this element until the mouse button is released or document.releaseCapture() is called.
    member x.SetCapture(retargetToElement : bool) =
        r.Invoke("setCapture", retargetToElement) |> ignore

    /// The setPointerCapture() method of the Element interface is used to designate a specific element as the capture target of future pointer events. Subsequent events for the pointer will be targeted at the capture element until capture is released (via Element.releasePointerCapture()).
    member x.SetPointerCapture(id : int) =
        r.Invoke("setPointerCapture", id) |> ignore

    /// The toggleAttribute() method of the Element interface toggles a Boolean attribute (removing it if it is present and adding it if it is not present) on the given element.
    member x.ToggleAttribute(name : string, ?force : bool) =
        match force with
        | Some f -> r.Invoke("toggleAttribute", f) |> convert<bool>
        | None -> r.Invoke("toggleAttribute") |> convert<bool>

    /// The cancel event fires on a <dialog> when the user instructs the browser that they wish to dismiss the current open dialog. For example, the browser might fire this event when the user presses the Esc key or clicks a "Close dialog" button which is part of the browser's UI.
    member x.OnCancel =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("cancel", obs.OnNext)
        }

    /// The error event is fired on an Element object when a resource failed to load, or can't be used. For example, if a script has an execution error or an image can't be found or is invalid.
    member x.OnError =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("error", obs.OnNext)
        }

    /// The scroll event fires an element has been scrolled.
    member x.OnScroll =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("scroll", obs.OnNext)
        }

    /// The select event fires when some text has been selected.
    member x.OnSelect =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("select", obs.OnNext)
        }

    /// The wheel event fires when the user rotates a wheel button on a pointing device (typically a mouse).
    member x.OnWheel =
        { new IObservable<WheelEvent> with
            member __.Subscribe(obs : IObserver<WheelEvent>) =
                x.SubscribeEventListener("wheel", WheelEvent >> obs.OnNext)
        }

    /// The copy event fires when the user initiates a copy action through the browser's user interface.
    member x.OnCopy =
        { new IObservable<ClipboardEvent> with
            member __.Subscribe(obs : IObserver<ClipboardEvent>) =
                x.SubscribeEventListener("copy", ClipboardEvent >> obs.OnNext)
        }

    /// The cut event is fired when the user has initiated a "cut" action through the browser's user interface.
    member x.OnCut =
        { new IObservable<ClipboardEvent> with
            member __.Subscribe(obs : IObserver<ClipboardEvent>) =
                x.SubscribeEventListener("cut", ClipboardEvent >> obs.OnNext)
        }

    /// The paste event is fired when the user has initiated a "paste" action through the browser's user interface.
    member x.OnPaste =
        { new IObservable<ClipboardEvent> with
            member __.Subscribe(obs : IObserver<ClipboardEvent>) =
                x.SubscribeEventListener("paste", ClipboardEvent >> obs.OnNext)
        }

    /// Fired when a text composition system such as an input method editor starts a new composition session.
    member x.OnCompositionStart =
        { new IObservable<CompositionEvent> with
            member __.Subscribe(obs : IObserver<CompositionEvent>) =
                x.SubscribeEventListener("compositionstart", CompositionEvent >> obs.OnNext)
        }
        
    /// Fired when a text composition system such as an input method editor completes or cancels the current composition session.
    member x.OnCompositionEnd =
        { new IObservable<CompositionEvent> with
            member __.Subscribe(obs : IObserver<CompositionEvent>) =
                x.SubscribeEventListener("compositionend", CompositionEvent >> obs.OnNext)
        }

    /// Fired when a new character is received in the context of a text composition session controlled by a text composition system such as an input method editor.
    member x.OnCompositionUpdate =
        { new IObservable<CompositionEvent> with
            member __.Subscribe(obs : IObserver<CompositionEvent>) =
                x.SubscribeEventListener("compositionupdate", CompositionEvent >> obs.OnNext)
        }

    /// Fired when an element has lost focus.
    member x.OnBlur =
        { new IObservable<FocusEvent> with
            member __.Subscribe(obs : IObserver<FocusEvent>) =
                x.SubscribeEventListener("blur", FocusEvent >> obs.OnNext)
        }

    /// Fired when an element has gained focus.
    member x.OnFocus =
        { new IObservable<FocusEvent> with
            member __.Subscribe(obs : IObserver<FocusEvent>) =
                x.SubscribeEventListener("focus", FocusEvent >> obs.OnNext)
        }
        
    /// Fired when an element is about to gain focus.
    member x.OnFocusIn =
        { new IObservable<FocusEvent> with
            member __.Subscribe(obs : IObserver<FocusEvent>) =
                x.SubscribeEventListener("focusin", FocusEvent >> obs.OnNext)
        }
        
    /// Fired when an element is about to lose focus.
    member x.OnFocusOut =
        { new IObservable<FocusEvent> with
            member __.Subscribe(obs : IObserver<FocusEvent>) =
                x.SubscribeEventListener("focusout", FocusEvent >> obs.OnNext)
        }

    /// The fullscreenchange event is fired immediately after an Element switches into or out of full-screen mode.
    member x.OnFullScreenChange =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("fullscreenchange", obs.OnNext)
        }

    /// Sent to an Element if an error occurs while attempting to switch it into or out of full-screen mode.
    member x.OnFullScreenError =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("fullscreenerror", obs.OnNext)
        }

    /// Fired when a key is pressed.
    member x.OnKeyDown =
        { new IObservable<KeyboardEvent> with
            member __.Subscribe(obs : IObserver<KeyboardEvent>) =
                x.SubscribeEventListener("keydown", KeyboardEvent >> obs.OnNext)
        }

    /// Fired when a key that produces a character value is pressed down.
    member x.OnKeyPress =
        { new IObservable<KeyboardEvent> with
            member __.Subscribe(obs : IObserver<KeyboardEvent>) =
                x.SubscribeEventListener("keypress", KeyboardEvent >> obs.OnNext)
        }

    /// Fired when a key is released.
    member x.OnKeyUp =
        { new IObservable<KeyboardEvent> with
            member __.Subscribe(obs : IObserver<KeyboardEvent>) =
                x.SubscribeEventListener("keyup", KeyboardEvent >> obs.OnNext)
        }

    /// Fired when a non-primary pointing device button (e.g., any mouse button other than the left button) has been pressed and released on an element.
    member x.OnAuxClick =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("auxclick", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device button (e.g., a mouse's primary button) is pressed and released on a single element.
    member x.OnClick =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("click", MouseEvent >> obs.OnNext)
        }
        
    /// Fired when the user attempts to open a context menu.
    member x.OnContextMenu =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("contextmenu", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device button (e.g., a mouse's primary button) is clicked twice on a single element.
    member x.OnDoubleClick =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("dblclick", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device button is pressed on an element.
    member x.OnMouseDown =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mousedown", MouseEvent >> obs.OnNext)
        }
        
    /// Fired when a pointing device (usually a mouse) is moved over the element that has the listener attached.
    member x.OnMouseEnter =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mouseenter", MouseEvent >> obs.OnNext)
        }

    /// Fired when the pointer of a pointing device (usually a mouse) is moved out of an element that has the listener attached to it.
    member x.OnMouseLeave =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mouseleave", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device (usually a mouse) is moved while over an element.
    member x.OnMouseMove =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mousemove", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device (usually a mouse) is moved off the element to which the listener is attached or off one of its children.
    member x.OnMouseOut =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mouseout", MouseEvent >> obs.OnNext)
        }

    /// Fired when a pointing device is moved onto the element to which the listener is attached or onto one of its children.
    member x.OnMouseOver =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mouseover", MouseEvent >> obs.OnNext)
        }
        
    /// Fired when a pointing device button is released on an element.
    member x.OnMouseUp =
        { new IObservable<MouseEvent> with
            member __.Subscribe(obs : IObserver<MouseEvent>) =
                x.SubscribeEventListener("mouseup", MouseEvent >> obs.OnNext)
        }
        
    /// Fired when one or more touch points have been disrupted in an implementation-specific manner (for example, too many touch points are created).
    member x.OnTouchCancel =
        { new IObservable<TouchEvent> with
            member __.Subscribe(obs : IObserver<TouchEvent>) =
                x.SubscribeEventListener("touchcancel", TouchEvent >> obs.OnNext)
        }
        
    /// Fired when one or more touch points are removed from the touch surface.
    member x.OnTouchEnd =
        { new IObservable<TouchEvent> with
            member __.Subscribe(obs : IObserver<TouchEvent>) =
                x.SubscribeEventListener("touchend", TouchEvent >> obs.OnNext)
        }
        
    /// Fired when one or more touch points are moved along the touch surface.
    member x.OnTouchMove =
        { new IObservable<TouchEvent> with
            member __.Subscribe(obs : IObserver<TouchEvent>) =
                x.SubscribeEventListener("touchmove", TouchEvent >> obs.OnNext)
        }

    /// Fired when one or more touch points are placed on the touch surface.
    member x.OnTouchStart =
        { new IObservable<TouchEvent> with
            member __.Subscribe(obs : IObserver<TouchEvent>) =
                x.SubscribeEventListener("touchstart", TouchEvent >> obs.OnNext)
        }

    /// Fired when the value of an <input>, <select>, or <textarea> element is about to be modified.
    member x.OnBeforeInput =
        { new IObservable<InputEvent> with
            member __.Subscribe(obs : IObserver<InputEvent>) =
                x.SubscribeEventListener("beforeinput", InputEvent >> obs.OnNext)
        }
        
    /// Fired when the value of an <input>, <select>, or <textarea> element has been changed.
    member x.OnInput =
        { new IObservable<InputEvent> with
            member __.Subscribe(obs : IObserver<InputEvent>) =
                x.SubscribeEventListener("input", InputEvent >> obs.OnNext)
        }

    /// Fired when an element captures a pointer using setPointerCapture().
    member x.OnGotPointerCapture =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("gotpointercapture", PointerEvent >> obs.OnNext)
        }

    /// Fired when a captured pointer is released.
    member x.OnLostPointerCapture =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("lostpointercapture", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer event is canceled.
    member x.OnPointerCancel =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointercancel", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer becomes active.
    member x.OnPointerDown =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerdown", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer is moved into the hit test boundaries of an element or one of its descendants.
    member x.OnPointerEnter =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerenter", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer is moved out of the hit test boundaries of an element.
    member x.OnPointerLeave =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerleave", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer changes coordinates.
    member x.OnPointerMove =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointermove", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer is moved out of the hit test boundaries of an element (among other reasons).
    member x.OnPointerOut =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerout", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer is moved into an element's hit test boundaries.
    member x.OnPointerOver =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerover", PointerEvent >> obs.OnNext)
        }

    /// Fired when a pointer is no longer active.
    member x.OnPointerUp =
        { new IObservable<PointerEvent> with
            member __.Subscribe(obs : IObserver<PointerEvent>) =
                x.SubscribeEventListener("pointerup", PointerEvent >> obs.OnNext)
        }
        

    // TODO: ShadowRoot
    //       OpenOrClosedShadowRoot
    //       Slot
    //       Animate
    //       GetAnimations
    //       AttachShadow
    //       CreateShadowRoot
    //       GetClientRects
    //       OnAnimationCancel
    //       OnAnimationEnd
    //       OnAnimationIteration
    //       OnAnimationStart
    //       OnTransitionCancel
    //       OnTransitionEnd
    //       OnTransitionRun
    //       OnTransitionStart

    new (o : JsObj) =
        Element o.Reference

[<AllowNullLiteral>]
type HTMLElement(r : JSObject) =
    inherit Element(r)
    
    /// Is a DOMString representing the access key assigned to the element.
    member x.AccessKey = r.GetObjectProperty "accessKey" |> convert<string>

    /// Returns a DOMString containing the element's assigned access key.
    member x.AccessKeyLabel = r.GetObjectProperty "accessKeyLabel" |> convert<string>

    /// Is a DOMString, where a value of true means the element is editable and a value of false means it isn't.
    member x.ContentEditable
        with get() = r.GetObjectProperty "contentEditable" |> convert<string>
        and set(v : string) = r.SetObjectProperty("contentEditable", v)

    /// Returns a Boolean that indicates whether or not the content of the element can be edited.
    member x.IsContentEditable
        with get() = r.GetObjectProperty "isContentEditable" |> convert<bool>
        and set (v : bool) =
            if v then x.ContentEditable <- "true"
            else x.ContentEditable <- "false"

    /// Returns a DOMStringMap with which script can read and write the element's custom data attributes (data-*) .
    member x.Dataset = r.GetObjectProperty "dataset" |> net // TODO: DOMStringMap

    /// Is a DOMString, reflecting the dir global attribute, representing the directionality of the element. Possible values are "ltr", "rtl", and "auto".
    member x.Dir
        with get() = r.GetObjectProperty "dir" |> convert<string>
        and set(v : string) = r.SetObjectProperty("dir", v)

    /// Is a Boolean indicating if the element can be dragged.
    member x.Draggable
        with get() = r.GetObjectProperty "draggable" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("draggable", v)

    /// Returns a DOMSettableTokenList reflecting the dropzone global attribute and describing the behavior of the element regarding a drop operation.
    member x.DropZone = r.GetObjectProperty("dropzone") |> net // TODO: DOMSettableTokenList

    /// Is a Boolean indicating if the element is hidden or not.
    member x.Hidden
        with get() = r.GetObjectProperty "hidden" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("hidden", v)
        
    /// Is a Boolean indicating whether the user agent must act as though the given node is absent for the purposes of user interaction events, in-page text searches ("find in page"), and text selection.
    member x.Inert
        with get() = r.GetObjectProperty "inert" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("inert", v)
        
    /// Represents the "rendered" text content of a node and its descendants. As a getter, it approximates the text the user would get if they highlighted the contents of the element with the cursor and then copied it to the clipboard.
    member x.InnerText
        with get() = r.GetObjectProperty "innerText" |> convert<string>
        and set (v : string) = r.SetObjectProperty("innerText", v)

    /// Is a Boolean representing the item scope.
    member x.ItemScope
        with get() = r.GetObjectProperty "itemScope" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("itemScope", v)

    /// Returns a DOMSettableTokenList...
    member x.ItemType
        with get() = r.GetObjectProperty "itemType" |> net
        and set (v : obj) = r.SetObjectProperty("itemType", js v) // TODO: DOMSettableTokenList
    
    /// Is a DOMString representing the item ID.
    member x.ItemId
        with get() = r.GetObjectProperty "itemType" |> convert<string>
        and set (v : string) = r.SetObjectProperty("itemType", v)
    
    /// Returns a DOMSettableTokenList...
    member x.ItemRef
        with get() = r.GetObjectProperty "itemRef" |> net
        and set (v : obj) = r.SetObjectProperty("itemRef", js v) // TODO: DOMSettableTokenList
    
    /// Returns a DOMSettableTokenList...
    member x.ItemProp
        with get() = r.GetObjectProperty "itemProp" |> net
        and set (v : obj) = r.SetObjectProperty("itemProp", js v) // TODO: DOMSettableTokenList

    /// Returns a Object representing the item value.
    member x.ItemValue
        with get() = r.GetObjectProperty "itemValue " |> net
        and set (v : obj) = r.SetObjectProperty("itemValue ", js v)
    
    /// Is a DOMString representing the language of an element's attributes, text, and element contents.
    member x.Lang
        with get() = r.GetObjectProperty "lang " |> convert<string>
        and set (v : string) = r.SetObjectProperty("lang ", v)
    
    /// Is a Boolean indicating whether an import script can be executed in user agents that support module scripts.
    member x.NoModule
        with get() = r.GetObjectProperty "noModule" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("noModule", v)
    
    /// Returns the cryptographic number used once that is used by Content Security Policy to determine whether a given fetch will be allowed to proceed.
    member x.Nonce
        with get() = r.GetObjectProperty "nonce" |> convert<string>
        and set (v : string) = r.SetObjectProperty("nonce", v)
        
        
    /// Returns a Element that is the element from which all offset calculations are currently computed.
    member x.OffsetParent = r.GetObjectProperty "offsetParent" |> convert<Element>
    /// Returns a double, the distance from this element's left border to its offsetParent's left border.
    member x.OffsetLeft = r.GetObjectProperty("offsetLeft") |> convert<float>
    /// Returns a double, the distance from this element's top border to its offsetParent's top border.
    member x.OffsetTop = r.GetObjectProperty("offsetTop") |> convert<float>
    /// Returns a double containing the width of an element, relative to the layout.
    member x.OffsetWidth = r.GetObjectProperty("offsetWidth") |> convert<float>
    /// Returns a double containing the height of an element, relative to the layout.
    member x.OffsetHeight = r.GetObjectProperty("offsetHeight") |> convert<float>

    /// Returns a HTMLPropertiesCollection…
    member x.Properties = r.GetObjectProperty "properties" |> net // TODO: HTMLPropertiesCollection

    /// Is a Boolean that controls spell-checking. It is present on all HTML elements, though it doesn't have an effect on all of them.
    member x.SpellCheck
        with get() = r.GetObjectProperty "spellcheck" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("spellcheck", v)
        
    /// Is a CSSStyleDeclaration, an object representing the declarations of an element's style attributes.
    member x.Style = r.GetObjectProperty "style" |> convert<CSSStyleDeclaration>

    /// Is a long representing the position of the element in the tabbing order.
    member x.TabIndex
        with get() = r.GetObjectProperty "tabIndex" |> convert<int>
        and set (v : int) = r.SetObjectProperty("tabIndex", v)
        
    /// Is a DOMString containing the text that appears in a popup box when mouse is over the element.
    member x.Title
        with get() = r.GetObjectProperty "title" |> convert<string>
        and set (v : string) = r.SetObjectProperty("title", v)

    /// Is a Boolean representing the translation.
    member x.Translate
        with get() = r.GetObjectProperty "translate" |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("translate", v)
        
    /// Removes keyboard focus from the currently focused element.
    member x.Blur() = r.Invoke("blur") |> ignore
    
    /// Sends a mouse click event to the element.
    member x.Click() = r.Invoke("click") |> ignore
    
    /// Makes the element the current keyboard focus.
    member x.Focus() = r.Invoke("focus") |> ignore
    
    /// Runs the spell checker on the element's contents.
    member x.ForceSpellCheck() = r.Invoke("forceSpellCheck") |> ignore

    /// Fired when an element does not satisfy its constraints during constraint validation.
    member x.OnInvalid =
        { new IObservable<Event> with
            member __.Subscribe(obs : IObserver<Event>) =
                x.SubscribeEventListener("invalid", obs.OnNext)
        }

    new (o : JsObj) =
        HTMLElement o.Reference
    

/// The CSSStyleDeclaration interface represents an object that is a CSS declaration block, and exposes style information and various style-related methods and properties.
[<AllowNullLiteral>]
type CSSStyleDeclaration(r : JSObject) =
    inherit JsObj(r)

    member x.CssText
        with get() : string = r.GetObjectProperty("cssText") |> convert
        and set(v : string)  = r.SetObjectProperty("cssText", v) |> convert

    member x.GetPropertyValue(name : string) =
        r.Invoke("getPropertyValue", name) |> convert<string>
        
    member x.SetProperty(name : string, value : string) =
        r.Invoke("setProperty", name, value) |> ignore

    member x.SetProperty(name : string, value : string, priority : string) =
        r.Invoke("setProperty", name, value, priority) |> ignore

    member x.RemoveProperty(name : string) =
        r.Invoke("removeProperty", name) |> ignore

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-accelerator
    member x.MsAccelerator
        with get() = x.GetPropertyValue("-ms-accelerator")
        and set v  = x.SetProperty("-ms-accelerator", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-block-progression
    member x.MsBlockProgression
        with get() = x.GetPropertyValue("-ms-block-progression")
        and set v  = x.SetProperty("-ms-block-progression", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-chaining
    member x.MsContentZoomChaining
        with get() = x.GetPropertyValue("-ms-content-zoom-chaining")
        and set v  = x.SetProperty("-ms-content-zoom-chaining", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zooming
    member x.MsContentZooming
        with get() = x.GetPropertyValue("-ms-content-zooming")
        and set v  = x.SetProperty("-ms-content-zooming", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-limit
    member x.MsContentZoomLimit
        with get() = x.GetPropertyValue("-ms-content-zoom-limit")
        and set v  = x.SetProperty("-ms-content-zoom-limit", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-limit-max
    member x.MsContentZoomLimitMax
        with get() = x.GetPropertyValue("-ms-content-zoom-limit-max")
        and set v  = x.SetProperty("-ms-content-zoom-limit-max", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-limit-min
    member x.MsContentZoomLimitMin
        with get() = x.GetPropertyValue("-ms-content-zoom-limit-min")
        and set v  = x.SetProperty("-ms-content-zoom-limit-min", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-snap
    member x.MsContentZoomSnap
        with get() = x.GetPropertyValue("-ms-content-zoom-snap")
        and set v  = x.SetProperty("-ms-content-zoom-snap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-snap-points
    member x.MsContentZoomSnapPoints
        with get() = x.GetPropertyValue("-ms-content-zoom-snap-points")
        and set v  = x.SetProperty("-ms-content-zoom-snap-points", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-content-zoom-snap-type
    member x.MsContentZoomSnapType
        with get() = x.GetPropertyValue("-ms-content-zoom-snap-type")
        and set v  = x.SetProperty("-ms-content-zoom-snap-type", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-filter
    member x.MsFilter
        with get() = x.GetPropertyValue("-ms-filter")
        and set v  = x.SetProperty("-ms-filter", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-flow-from
    member x.MsFlowFrom
        with get() = x.GetPropertyValue("-ms-flow-from")
        and set v  = x.SetProperty("-ms-flow-from", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-flow-into
    member x.MsFlowInto
        with get() = x.GetPropertyValue("-ms-flow-into")
        and set v  = x.SetProperty("-ms-flow-into", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-high-contrast-adjust
    member x.MsHighContrastAdjust
        with get() = x.GetPropertyValue("-ms-high-contrast-adjust")
        and set v  = x.SetProperty("-ms-high-contrast-adjust", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-hyphenate-limit-chars
    member x.MsHyphenateLimitChars
        with get() = x.GetPropertyValue("-ms-hyphenate-limit-chars")
        and set v  = x.SetProperty("-ms-hyphenate-limit-chars", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-hyphenate-limit-lines
    member x.MsHyphenateLimitLines
        with get() = x.GetPropertyValue("-ms-hyphenate-limit-lines")
        and set v  = x.SetProperty("-ms-hyphenate-limit-lines", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-hyphenate-limit-zone
    member x.MsHyphenateLimitZone
        with get() = x.GetPropertyValue("-ms-hyphenate-limit-zone")
        and set v  = x.SetProperty("-ms-hyphenate-limit-zone", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-ime-align
    member x.MsImeAlign
        with get() = x.GetPropertyValue("-ms-ime-align")
        and set v  = x.SetProperty("-ms-ime-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-overflow-style
    member x.MsOverflowStyle
        with get() = x.GetPropertyValue("-ms-overflow-style")
        and set v  = x.SetProperty("-ms-overflow-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-3dlight-color
    member x.MsScrollbar3dlightColor
        with get() = x.GetPropertyValue("-ms-scrollbar-3dlight-color")
        and set v  = x.SetProperty("-ms-scrollbar-3dlight-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-arrow-color
    member x.MsScrollbarArrowColor
        with get() = x.GetPropertyValue("-ms-scrollbar-arrow-color")
        and set v  = x.SetProperty("-ms-scrollbar-arrow-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-base-color
    member x.MsScrollbarBaseColor
        with get() = x.GetPropertyValue("-ms-scrollbar-base-color")
        and set v  = x.SetProperty("-ms-scrollbar-base-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-darkshadow-color
    member x.MsScrollbarDarkshadowColor
        with get() = x.GetPropertyValue("-ms-scrollbar-darkshadow-color")
        and set v  = x.SetProperty("-ms-scrollbar-darkshadow-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-face-color
    member x.MsScrollbarFaceColor
        with get() = x.GetPropertyValue("-ms-scrollbar-face-color")
        and set v  = x.SetProperty("-ms-scrollbar-face-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-highlight-color
    member x.MsScrollbarHighlightColor
        with get() = x.GetPropertyValue("-ms-scrollbar-highlight-color")
        and set v  = x.SetProperty("-ms-scrollbar-highlight-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-shadow-color
    member x.MsScrollbarShadowColor
        with get() = x.GetPropertyValue("-ms-scrollbar-shadow-color")
        and set v  = x.SetProperty("-ms-scrollbar-shadow-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scrollbar-track-color
    member x.MsScrollbarTrackColor
        with get() = x.GetPropertyValue("-ms-scrollbar-track-color")
        and set v  = x.SetProperty("-ms-scrollbar-track-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-chaining
    member x.MsScrollChaining
        with get() = x.GetPropertyValue("-ms-scroll-chaining")
        and set v  = x.SetProperty("-ms-scroll-chaining", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-limit
    member x.MsScrollLimit
        with get() = x.GetPropertyValue("-ms-scroll-limit")
        and set v  = x.SetProperty("-ms-scroll-limit", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-limit-x-max
    member x.MsScrollLimitXMax
        with get() = x.GetPropertyValue("-ms-scroll-limit-x-max")
        and set v  = x.SetProperty("-ms-scroll-limit-x-max", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-limit-x-min
    member x.MsScrollLimitXMin
        with get() = x.GetPropertyValue("-ms-scroll-limit-x-min")
        and set v  = x.SetProperty("-ms-scroll-limit-x-min", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-limit-y-max
    member x.MsScrollLimitYMax
        with get() = x.GetPropertyValue("-ms-scroll-limit-y-max")
        and set v  = x.SetProperty("-ms-scroll-limit-y-max", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-limit-y-min
    member x.MsScrollLimitYMin
        with get() = x.GetPropertyValue("-ms-scroll-limit-y-min")
        and set v  = x.SetProperty("-ms-scroll-limit-y-min", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-rails
    member x.MsScrollRails
        with get() = x.GetPropertyValue("-ms-scroll-rails")
        and set v  = x.SetProperty("-ms-scroll-rails", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-snap-points-x
    member x.MsScrollSnapPointsX
        with get() = x.GetPropertyValue("-ms-scroll-snap-points-x")
        and set v  = x.SetProperty("-ms-scroll-snap-points-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-snap-points-y
    member x.MsScrollSnapPointsY
        with get() = x.GetPropertyValue("-ms-scroll-snap-points-y")
        and set v  = x.SetProperty("-ms-scroll-snap-points-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-snap-type
    member x.MsScrollSnapType
        with get() = x.GetPropertyValue("-ms-scroll-snap-type")
        and set v  = x.SetProperty("-ms-scroll-snap-type", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-snap-x
    member x.MsScrollSnapX
        with get() = x.GetPropertyValue("-ms-scroll-snap-x")
        and set v  = x.SetProperty("-ms-scroll-snap-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-snap-y
    member x.MsScrollSnapY
        with get() = x.GetPropertyValue("-ms-scroll-snap-y")
        and set v  = x.SetProperty("-ms-scroll-snap-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-scroll-translation
    member x.MsScrollTranslation
        with get() = x.GetPropertyValue("-ms-scroll-translation")
        and set v  = x.SetProperty("-ms-scroll-translation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-text-autospace
    member x.MsTextAutospace
        with get() = x.GetPropertyValue("-ms-text-autospace")
        and set v  = x.SetProperty("-ms-text-autospace", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-touch-select
    member x.MsTouchSelect
        with get() = x.GetPropertyValue("-ms-touch-select")
        and set v  = x.SetProperty("-ms-touch-select", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-user-select
    member x.MsUserSelect
        with get() = x.GetPropertyValue("-ms-user-select")
        and set v  = x.SetProperty("-ms-user-select", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-wrap-flow
    member x.MsWrapFlow
        with get() = x.GetPropertyValue("-ms-wrap-flow")
        and set v  = x.SetProperty("-ms-wrap-flow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-wrap-margin
    member x.MsWrapMargin
        with get() = x.GetPropertyValue("-ms-wrap-margin")
        and set v  = x.SetProperty("-ms-wrap-margin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-ms-wrap-through
    member x.MsWrapThrough
        with get() = x.GetPropertyValue("-ms-wrap-through")
        and set v  = x.SetProperty("-ms-wrap-through", v)

    /// https://developer.mozilla.org/docs/Web/CSS/appearance
    member x.MozAppearance
        with get() = x.GetPropertyValue("-moz-appearance")
        and set v  = x.SetProperty("-moz-appearance", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-binding
    member x.MozBinding
        with get() = x.GetPropertyValue("-moz-binding")
        and set v  = x.SetProperty("-moz-binding", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-border-bottom-colors
    member x.MozBorderBottomColors
        with get() = x.GetPropertyValue("-moz-border-bottom-colors")
        and set v  = x.SetProperty("-moz-border-bottom-colors", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-border-left-colors
    member x.MozBorderLeftColors
        with get() = x.GetPropertyValue("-moz-border-left-colors")
        and set v  = x.SetProperty("-moz-border-left-colors", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-border-right-colors
    member x.MozBorderRightColors
        with get() = x.GetPropertyValue("-moz-border-right-colors")
        and set v  = x.SetProperty("-moz-border-right-colors", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-border-top-colors
    member x.MozBorderTopColors
        with get() = x.GetPropertyValue("-moz-border-top-colors")
        and set v  = x.SetProperty("-moz-border-top-colors", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-context-properties
    member x.MozContextProperties
        with get() = x.GetPropertyValue("-moz-context-properties")
        and set v  = x.SetProperty("-moz-context-properties", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-float-edge
    member x.MozFloatEdge
        with get() = x.GetPropertyValue("-moz-float-edge")
        and set v  = x.SetProperty("-moz-float-edge", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-force-broken-image-icon
    member x.MozForceBrokenImageIcon
        with get() = x.GetPropertyValue("-moz-force-broken-image-icon")
        and set v  = x.SetProperty("-moz-force-broken-image-icon", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-image-region
    member x.MozImageRegion
        with get() = x.GetPropertyValue("-moz-image-region")
        and set v  = x.SetProperty("-moz-image-region", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-orient
    member x.MozOrient
        with get() = x.GetPropertyValue("-moz-orient")
        and set v  = x.SetProperty("-moz-orient", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-outline-radius
    member x.MozOutlineRadius
        with get() = x.GetPropertyValue("-moz-outline-radius")
        and set v  = x.SetProperty("-moz-outline-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-outline-radius-bottomleft
    member x.MozOutlineRadiusBottomleft
        with get() = x.GetPropertyValue("-moz-outline-radius-bottomleft")
        and set v  = x.SetProperty("-moz-outline-radius-bottomleft", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-outline-radius-bottomright
    member x.MozOutlineRadiusBottomright
        with get() = x.GetPropertyValue("-moz-outline-radius-bottomright")
        and set v  = x.SetProperty("-moz-outline-radius-bottomright", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-outline-radius-topleft
    member x.MozOutlineRadiusTopleft
        with get() = x.GetPropertyValue("-moz-outline-radius-topleft")
        and set v  = x.SetProperty("-moz-outline-radius-topleft", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-outline-radius-topright
    member x.MozOutlineRadiusTopright
        with get() = x.GetPropertyValue("-moz-outline-radius-topright")
        and set v  = x.SetProperty("-moz-outline-radius-topright", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-stack-sizing
    member x.MozStackSizing
        with get() = x.GetPropertyValue("-moz-stack-sizing")
        and set v  = x.SetProperty("-moz-stack-sizing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-text-blink
    member x.MozTextBlink
        with get() = x.GetPropertyValue("-moz-text-blink")
        and set v  = x.SetProperty("-moz-text-blink", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-user-focus
    member x.MozUserFocus
        with get() = x.GetPropertyValue("-moz-user-focus")
        and set v  = x.SetProperty("-moz-user-focus", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-user-input
    member x.MozUserInput
        with get() = x.GetPropertyValue("-moz-user-input")
        and set v  = x.SetProperty("-moz-user-input", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-user-modify
    member x.MozUserModify
        with get() = x.GetPropertyValue("-moz-user-modify")
        and set v  = x.SetProperty("-moz-user-modify", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-window-dragging
    member x.MozWindowDragging
        with get() = x.GetPropertyValue("-moz-window-dragging")
        and set v  = x.SetProperty("-moz-window-dragging", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-moz-window-shadow
    member x.MozWindowShadow
        with get() = x.GetPropertyValue("-moz-window-shadow")
        and set v  = x.SetProperty("-moz-window-shadow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/appearance
    member x.WebkitAppearance
        with get() = x.GetPropertyValue("-webkit-appearance")
        and set v  = x.SetProperty("-webkit-appearance", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-border-before
    member x.WebkitBorderBefore
        with get() = x.GetPropertyValue("-webkit-border-before")
        and set v  = x.SetProperty("-webkit-border-before", v)

    member x.WebkitBorderBeforeColor
        with get() = x.GetPropertyValue("-webkit-border-before-color")
        and set v  = x.SetProperty("-webkit-border-before-color", v)

    member x.WebkitBorderBeforeStyle
        with get() = x.GetPropertyValue("-webkit-border-before-style")
        and set v  = x.SetProperty("-webkit-border-before-style", v)

    member x.WebkitBorderBeforeWidth
        with get() = x.GetPropertyValue("-webkit-border-before-width")
        and set v  = x.SetProperty("-webkit-border-before-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-box-reflect
    member x.WebkitBoxReflect
        with get() = x.GetPropertyValue("-webkit-box-reflect")
        and set v  = x.SetProperty("-webkit-box-reflect", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-line-clamp
    member x.WebkitLineClamp
        with get() = x.GetPropertyValue("-webkit-line-clamp")
        and set v  = x.SetProperty("-webkit-line-clamp", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask
    member x.WebkitMask
        with get() = x.GetPropertyValue("-webkit-mask")
        and set v  = x.SetProperty("-webkit-mask", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-attachment
    member x.WebkitMaskAttachment
        with get() = x.GetPropertyValue("-webkit-mask-attachment")
        and set v  = x.SetProperty("-webkit-mask-attachment", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-clip
    member x.WebkitMaskClip
        with get() = x.GetPropertyValue("-webkit-mask-clip")
        and set v  = x.SetProperty("-webkit-mask-clip", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-composite
    member x.WebkitMaskComposite
        with get() = x.GetPropertyValue("-webkit-mask-composite")
        and set v  = x.SetProperty("-webkit-mask-composite", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-image
    member x.WebkitMaskImage
        with get() = x.GetPropertyValue("-webkit-mask-image")
        and set v  = x.SetProperty("-webkit-mask-image", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-origin
    member x.WebkitMaskOrigin
        with get() = x.GetPropertyValue("-webkit-mask-origin")
        and set v  = x.SetProperty("-webkit-mask-origin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-position
    member x.WebkitMaskPosition
        with get() = x.GetPropertyValue("-webkit-mask-position")
        and set v  = x.SetProperty("-webkit-mask-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-position-x
    member x.WebkitMaskPositionX
        with get() = x.GetPropertyValue("-webkit-mask-position-x")
        and set v  = x.SetProperty("-webkit-mask-position-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-position-y
    member x.WebkitMaskPositionY
        with get() = x.GetPropertyValue("-webkit-mask-position-y")
        and set v  = x.SetProperty("-webkit-mask-position-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-repeat
    member x.WebkitMaskRepeat
        with get() = x.GetPropertyValue("-webkit-mask-repeat")
        and set v  = x.SetProperty("-webkit-mask-repeat", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-repeat-x
    member x.WebkitMaskRepeatX
        with get() = x.GetPropertyValue("-webkit-mask-repeat-x")
        and set v  = x.SetProperty("-webkit-mask-repeat-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-mask-repeat-y
    member x.WebkitMaskRepeatY
        with get() = x.GetPropertyValue("-webkit-mask-repeat-y")
        and set v  = x.SetProperty("-webkit-mask-repeat-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-size
    member x.WebkitMaskSize
        with get() = x.GetPropertyValue("-webkit-mask-size")
        and set v  = x.SetProperty("-webkit-mask-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-overflow-scrolling
    member x.WebkitOverflowScrolling
        with get() = x.GetPropertyValue("-webkit-overflow-scrolling")
        and set v  = x.SetProperty("-webkit-overflow-scrolling", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-tap-highlight-color
    member x.WebkitTapHighlightColor
        with get() = x.GetPropertyValue("-webkit-tap-highlight-color")
        and set v  = x.SetProperty("-webkit-tap-highlight-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-text-fill-color
    member x.WebkitTextFillColor
        with get() = x.GetPropertyValue("-webkit-text-fill-color")
        and set v  = x.SetProperty("-webkit-text-fill-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-text-stroke
    member x.WebkitTextStroke
        with get() = x.GetPropertyValue("-webkit-text-stroke")
        and set v  = x.SetProperty("-webkit-text-stroke", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-text-stroke-color
    member x.WebkitTextStrokeColor
        with get() = x.GetPropertyValue("-webkit-text-stroke-color")
        and set v  = x.SetProperty("-webkit-text-stroke-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-text-stroke-width
    member x.WebkitTextStrokeWidth
        with get() = x.GetPropertyValue("-webkit-text-stroke-width")
        and set v  = x.SetProperty("-webkit-text-stroke-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/-webkit-touch-callout
    member x.WebkitTouchCallout
        with get() = x.GetPropertyValue("-webkit-touch-callout")
        and set v  = x.SetProperty("-webkit-touch-callout", v)

    member x.WebkitUserModify
        with get() = x.GetPropertyValue("-webkit-user-modify")
        and set v  = x.SetProperty("-webkit-user-modify", v)

    /// https://developer.mozilla.org/docs/Web/CSS/align-content
    member x.AlignContent
        with get() = x.GetPropertyValue("align-content")
        and set v  = x.SetProperty("align-content", v)

    /// https://developer.mozilla.org/docs/Web/CSS/align-items
    member x.AlignItems
        with get() = x.GetPropertyValue("align-items")
        and set v  = x.SetProperty("align-items", v)

    /// https://developer.mozilla.org/docs/Web/CSS/align-self
    member x.AlignSelf
        with get() = x.GetPropertyValue("align-self")
        and set v  = x.SetProperty("align-self", v)

    /// https://developer.mozilla.org/docs/Web/CSS/all
    member x.All
        with get() = x.GetPropertyValue("all")
        and set v  = x.SetProperty("all", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation
    member x.Animation
        with get() = x.GetPropertyValue("animation")
        and set v  = x.SetProperty("animation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-delay
    member x.AnimationDelay
        with get() = x.GetPropertyValue("animation-delay")
        and set v  = x.SetProperty("animation-delay", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-direction
    member x.AnimationDirection
        with get() = x.GetPropertyValue("animation-direction")
        and set v  = x.SetProperty("animation-direction", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-duration
    member x.AnimationDuration
        with get() = x.GetPropertyValue("animation-duration")
        and set v  = x.SetProperty("animation-duration", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-fill-mode
    member x.AnimationFillMode
        with get() = x.GetPropertyValue("animation-fill-mode")
        and set v  = x.SetProperty("animation-fill-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-iteration-count
    member x.AnimationIterationCount
        with get() = x.GetPropertyValue("animation-iteration-count")
        and set v  = x.SetProperty("animation-iteration-count", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-name
    member x.AnimationName
        with get() = x.GetPropertyValue("animation-name")
        and set v  = x.SetProperty("animation-name", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-play-state
    member x.AnimationPlayState
        with get() = x.GetPropertyValue("animation-play-state")
        and set v  = x.SetProperty("animation-play-state", v)

    /// https://developer.mozilla.org/docs/Web/CSS/animation-timing-function
    member x.AnimationTimingFunction
        with get() = x.GetPropertyValue("animation-timing-function")
        and set v  = x.SetProperty("animation-timing-function", v)

    /// https://developer.mozilla.org/docs/Web/CSS/appearance
    member x.Appearance
        with get() = x.GetPropertyValue("appearance")
        and set v  = x.SetProperty("appearance", v)

    /// https://developer.mozilla.org/docs/Web/CSS/aspect-ratio
    member x.AspectRatio
        with get() = x.GetPropertyValue("aspect-ratio")
        and set v  = x.SetProperty("aspect-ratio", v)

    /// https://developer.mozilla.org/docs/Web/CSS/azimuth
    member x.Azimuth
        with get() = x.GetPropertyValue("azimuth")
        and set v  = x.SetProperty("azimuth", v)

    /// https://developer.mozilla.org/docs/Web/CSS/backdrop-filter
    member x.BackdropFilter
        with get() = x.GetPropertyValue("backdrop-filter")
        and set v  = x.SetProperty("backdrop-filter", v)

    /// https://developer.mozilla.org/docs/Web/CSS/backface-visibility
    member x.BackfaceVisibility
        with get() = x.GetPropertyValue("backface-visibility")
        and set v  = x.SetProperty("backface-visibility", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background
    member x.Background
        with get() = x.GetPropertyValue("background")
        and set v  = x.SetProperty("background", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-attachment
    member x.BackgroundAttachment
        with get() = x.GetPropertyValue("background-attachment")
        and set v  = x.SetProperty("background-attachment", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-blend-mode
    member x.BackgroundBlendMode
        with get() = x.GetPropertyValue("background-blend-mode")
        and set v  = x.SetProperty("background-blend-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-clip
    member x.BackgroundClip
        with get() = x.GetPropertyValue("background-clip")
        and set v  = x.SetProperty("background-clip", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-color
    member x.BackgroundColor
        with get() = x.GetPropertyValue("background-color")
        and set v  = x.SetProperty("background-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-image
    member x.BackgroundImage
        with get() = x.GetPropertyValue("background-image")
        and set v  = x.SetProperty("background-image", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-origin
    member x.BackgroundOrigin
        with get() = x.GetPropertyValue("background-origin")
        and set v  = x.SetProperty("background-origin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-position
    member x.BackgroundPosition
        with get() = x.GetPropertyValue("background-position")
        and set v  = x.SetProperty("background-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-position-x
    member x.BackgroundPositionX
        with get() = x.GetPropertyValue("background-position-x")
        and set v  = x.SetProperty("background-position-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-position-y
    member x.BackgroundPositionY
        with get() = x.GetPropertyValue("background-position-y")
        and set v  = x.SetProperty("background-position-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-repeat
    member x.BackgroundRepeat
        with get() = x.GetPropertyValue("background-repeat")
        and set v  = x.SetProperty("background-repeat", v)

    /// https://developer.mozilla.org/docs/Web/CSS/background-size
    member x.BackgroundSize
        with get() = x.GetPropertyValue("background-size")
        and set v  = x.SetProperty("background-size", v)

    member x.BlockOverflow
        with get() = x.GetPropertyValue("block-overflow")
        and set v  = x.SetProperty("block-overflow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/block-size
    member x.BlockSize
        with get() = x.GetPropertyValue("block-size")
        and set v  = x.SetProperty("block-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border
    member x.Border
        with get() = x.GetPropertyValue("border")
        and set v  = x.SetProperty("border", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block
    member x.BorderBlock
        with get() = x.GetPropertyValue("border-block")
        and set v  = x.SetProperty("border-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-color
    member x.BorderBlockColor
        with get() = x.GetPropertyValue("border-block-color")
        and set v  = x.SetProperty("border-block-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-style
    member x.BorderBlockStyle
        with get() = x.GetPropertyValue("border-block-style")
        and set v  = x.SetProperty("border-block-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-width
    member x.BorderBlockWidth
        with get() = x.GetPropertyValue("border-block-width")
        and set v  = x.SetProperty("border-block-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-end
    member x.BorderBlockEnd
        with get() = x.GetPropertyValue("border-block-end")
        and set v  = x.SetProperty("border-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-end-color
    member x.BorderBlockEndColor
        with get() = x.GetPropertyValue("border-block-end-color")
        and set v  = x.SetProperty("border-block-end-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-end-style
    member x.BorderBlockEndStyle
        with get() = x.GetPropertyValue("border-block-end-style")
        and set v  = x.SetProperty("border-block-end-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-end-width
    member x.BorderBlockEndWidth
        with get() = x.GetPropertyValue("border-block-end-width")
        and set v  = x.SetProperty("border-block-end-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-start
    member x.BorderBlockStart
        with get() = x.GetPropertyValue("border-block-start")
        and set v  = x.SetProperty("border-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-start-color
    member x.BorderBlockStartColor
        with get() = x.GetPropertyValue("border-block-start-color")
        and set v  = x.SetProperty("border-block-start-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-start-style
    member x.BorderBlockStartStyle
        with get() = x.GetPropertyValue("border-block-start-style")
        and set v  = x.SetProperty("border-block-start-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-block-start-width
    member x.BorderBlockStartWidth
        with get() = x.GetPropertyValue("border-block-start-width")
        and set v  = x.SetProperty("border-block-start-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom
    member x.BorderBottom
        with get() = x.GetPropertyValue("border-bottom")
        and set v  = x.SetProperty("border-bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom-color
    member x.BorderBottomColor
        with get() = x.GetPropertyValue("border-bottom-color")
        and set v  = x.SetProperty("border-bottom-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom-left-radius
    member x.BorderBottomLeftRadius
        with get() = x.GetPropertyValue("border-bottom-left-radius")
        and set v  = x.SetProperty("border-bottom-left-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom-right-radius
    member x.BorderBottomRightRadius
        with get() = x.GetPropertyValue("border-bottom-right-radius")
        and set v  = x.SetProperty("border-bottom-right-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom-style
    member x.BorderBottomStyle
        with get() = x.GetPropertyValue("border-bottom-style")
        and set v  = x.SetProperty("border-bottom-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-bottom-width
    member x.BorderBottomWidth
        with get() = x.GetPropertyValue("border-bottom-width")
        and set v  = x.SetProperty("border-bottom-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-collapse
    member x.BorderCollapse
        with get() = x.GetPropertyValue("border-collapse")
        and set v  = x.SetProperty("border-collapse", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-color
    member x.BorderColor
        with get() = x.GetPropertyValue("border-color")
        and set v  = x.SetProperty("border-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-end-end-radius
    member x.BorderEndEndRadius
        with get() = x.GetPropertyValue("border-end-end-radius")
        and set v  = x.SetProperty("border-end-end-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-end-start-radius
    member x.BorderEndStartRadius
        with get() = x.GetPropertyValue("border-end-start-radius")
        and set v  = x.SetProperty("border-end-start-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image
    member x.BorderImage
        with get() = x.GetPropertyValue("border-image")
        and set v  = x.SetProperty("border-image", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image-outset
    member x.BorderImageOutset
        with get() = x.GetPropertyValue("border-image-outset")
        and set v  = x.SetProperty("border-image-outset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image-repeat
    member x.BorderImageRepeat
        with get() = x.GetPropertyValue("border-image-repeat")
        and set v  = x.SetProperty("border-image-repeat", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image-slice
    member x.BorderImageSlice
        with get() = x.GetPropertyValue("border-image-slice")
        and set v  = x.SetProperty("border-image-slice", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image-source
    member x.BorderImageSource
        with get() = x.GetPropertyValue("border-image-source")
        and set v  = x.SetProperty("border-image-source", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-image-width
    member x.BorderImageWidth
        with get() = x.GetPropertyValue("border-image-width")
        and set v  = x.SetProperty("border-image-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline
    member x.BorderInline
        with get() = x.GetPropertyValue("border-inline")
        and set v  = x.SetProperty("border-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-end
    member x.BorderInlineEnd
        with get() = x.GetPropertyValue("border-inline-end")
        and set v  = x.SetProperty("border-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-color
    member x.BorderInlineColor
        with get() = x.GetPropertyValue("border-inline-color")
        and set v  = x.SetProperty("border-inline-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-style
    member x.BorderInlineStyle
        with get() = x.GetPropertyValue("border-inline-style")
        and set v  = x.SetProperty("border-inline-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-width
    member x.BorderInlineWidth
        with get() = x.GetPropertyValue("border-inline-width")
        and set v  = x.SetProperty("border-inline-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-end-color
    member x.BorderInlineEndColor
        with get() = x.GetPropertyValue("border-inline-end-color")
        and set v  = x.SetProperty("border-inline-end-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-end-style
    member x.BorderInlineEndStyle
        with get() = x.GetPropertyValue("border-inline-end-style")
        and set v  = x.SetProperty("border-inline-end-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-end-width
    member x.BorderInlineEndWidth
        with get() = x.GetPropertyValue("border-inline-end-width")
        and set v  = x.SetProperty("border-inline-end-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-start
    member x.BorderInlineStart
        with get() = x.GetPropertyValue("border-inline-start")
        and set v  = x.SetProperty("border-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-start-color
    member x.BorderInlineStartColor
        with get() = x.GetPropertyValue("border-inline-start-color")
        and set v  = x.SetProperty("border-inline-start-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-start-style
    member x.BorderInlineStartStyle
        with get() = x.GetPropertyValue("border-inline-start-style")
        and set v  = x.SetProperty("border-inline-start-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-inline-start-width
    member x.BorderInlineStartWidth
        with get() = x.GetPropertyValue("border-inline-start-width")
        and set v  = x.SetProperty("border-inline-start-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-left
    member x.BorderLeft
        with get() = x.GetPropertyValue("border-left")
        and set v  = x.SetProperty("border-left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-left-color
    member x.BorderLeftColor
        with get() = x.GetPropertyValue("border-left-color")
        and set v  = x.SetProperty("border-left-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-left-style
    member x.BorderLeftStyle
        with get() = x.GetPropertyValue("border-left-style")
        and set v  = x.SetProperty("border-left-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-left-width
    member x.BorderLeftWidth
        with get() = x.GetPropertyValue("border-left-width")
        and set v  = x.SetProperty("border-left-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-radius
    member x.BorderRadius
        with get() = x.GetPropertyValue("border-radius")
        and set v  = x.SetProperty("border-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-right
    member x.BorderRight
        with get() = x.GetPropertyValue("border-right")
        and set v  = x.SetProperty("border-right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-right-color
    member x.BorderRightColor
        with get() = x.GetPropertyValue("border-right-color")
        and set v  = x.SetProperty("border-right-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-right-style
    member x.BorderRightStyle
        with get() = x.GetPropertyValue("border-right-style")
        and set v  = x.SetProperty("border-right-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-right-width
    member x.BorderRightWidth
        with get() = x.GetPropertyValue("border-right-width")
        and set v  = x.SetProperty("border-right-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-spacing
    member x.BorderSpacing
        with get() = x.GetPropertyValue("border-spacing")
        and set v  = x.SetProperty("border-spacing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-start-end-radius
    member x.BorderStartEndRadius
        with get() = x.GetPropertyValue("border-start-end-radius")
        and set v  = x.SetProperty("border-start-end-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-start-start-radius
    member x.BorderStartStartRadius
        with get() = x.GetPropertyValue("border-start-start-radius")
        and set v  = x.SetProperty("border-start-start-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-style
    member x.BorderStyle
        with get() = x.GetPropertyValue("border-style")
        and set v  = x.SetProperty("border-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top
    member x.BorderTop
        with get() = x.GetPropertyValue("border-top")
        and set v  = x.SetProperty("border-top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top-color
    member x.BorderTopColor
        with get() = x.GetPropertyValue("border-top-color")
        and set v  = x.SetProperty("border-top-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top-left-radius
    member x.BorderTopLeftRadius
        with get() = x.GetPropertyValue("border-top-left-radius")
        and set v  = x.SetProperty("border-top-left-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top-right-radius
    member x.BorderTopRightRadius
        with get() = x.GetPropertyValue("border-top-right-radius")
        and set v  = x.SetProperty("border-top-right-radius", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top-style
    member x.BorderTopStyle
        with get() = x.GetPropertyValue("border-top-style")
        and set v  = x.SetProperty("border-top-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-top-width
    member x.BorderTopWidth
        with get() = x.GetPropertyValue("border-top-width")
        and set v  = x.SetProperty("border-top-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/border-width
    member x.BorderWidth
        with get() = x.GetPropertyValue("border-width")
        and set v  = x.SetProperty("border-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/bottom
    member x.Bottom
        with get() = x.GetPropertyValue("bottom")
        and set v  = x.SetProperty("bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-align
    member x.BoxAlign
        with get() = x.GetPropertyValue("box-align")
        and set v  = x.SetProperty("box-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-decoration-break
    member x.BoxDecorationBreak
        with get() = x.GetPropertyValue("box-decoration-break")
        and set v  = x.SetProperty("box-decoration-break", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-direction
    member x.BoxDirection
        with get() = x.GetPropertyValue("box-direction")
        and set v  = x.SetProperty("box-direction", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-flex
    member x.BoxFlex
        with get() = x.GetPropertyValue("box-flex")
        and set v  = x.SetProperty("box-flex", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-flex-group
    member x.BoxFlexGroup
        with get() = x.GetPropertyValue("box-flex-group")
        and set v  = x.SetProperty("box-flex-group", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-lines
    member x.BoxLines
        with get() = x.GetPropertyValue("box-lines")
        and set v  = x.SetProperty("box-lines", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-ordinal-group
    member x.BoxOrdinalGroup
        with get() = x.GetPropertyValue("box-ordinal-group")
        and set v  = x.SetProperty("box-ordinal-group", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-orient
    member x.BoxOrient
        with get() = x.GetPropertyValue("box-orient")
        and set v  = x.SetProperty("box-orient", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-pack
    member x.BoxPack
        with get() = x.GetPropertyValue("box-pack")
        and set v  = x.SetProperty("box-pack", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-shadow
    member x.BoxShadow
        with get() = x.GetPropertyValue("box-shadow")
        and set v  = x.SetProperty("box-shadow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/box-sizing
    member x.BoxSizing
        with get() = x.GetPropertyValue("box-sizing")
        and set v  = x.SetProperty("box-sizing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/break-after
    member x.BreakAfter
        with get() = x.GetPropertyValue("break-after")
        and set v  = x.SetProperty("break-after", v)

    /// https://developer.mozilla.org/docs/Web/CSS/break-before
    member x.BreakBefore
        with get() = x.GetPropertyValue("break-before")
        and set v  = x.SetProperty("break-before", v)

    /// https://developer.mozilla.org/docs/Web/CSS/break-inside
    member x.BreakInside
        with get() = x.GetPropertyValue("break-inside")
        and set v  = x.SetProperty("break-inside", v)

    /// https://developer.mozilla.org/docs/Web/CSS/caption-side
    member x.CaptionSide
        with get() = x.GetPropertyValue("caption-side")
        and set v  = x.SetProperty("caption-side", v)

    /// https://developer.mozilla.org/docs/Web/CSS/caret-color
    member x.CaretColor
        with get() = x.GetPropertyValue("caret-color")
        and set v  = x.SetProperty("caret-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/clear
    member x.Clear
        with get() = x.GetPropertyValue("clear")
        and set v  = x.SetProperty("clear", v)

    /// https://developer.mozilla.org/docs/Web/CSS/clip
    member x.Clip
        with get() = x.GetPropertyValue("clip")
        and set v  = x.SetProperty("clip", v)

    /// https://developer.mozilla.org/docs/Web/CSS/clip-path
    member x.ClipPath
        with get() = x.GetPropertyValue("clip-path")
        and set v  = x.SetProperty("clip-path", v)

    /// https://developer.mozilla.org/docs/Web/CSS/color
    member x.Color
        with get() = x.GetPropertyValue("color")
        and set v  = x.SetProperty("color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/color-adjust
    member x.ColorAdjust
        with get() = x.GetPropertyValue("color-adjust")
        and set v  = x.SetProperty("color-adjust", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-count
    member x.ColumnCount
        with get() = x.GetPropertyValue("column-count")
        and set v  = x.SetProperty("column-count", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-fill
    member x.ColumnFill
        with get() = x.GetPropertyValue("column-fill")
        and set v  = x.SetProperty("column-fill", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-gap
    member x.ColumnGap
        with get() = x.GetPropertyValue("column-gap")
        and set v  = x.SetProperty("column-gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-rule
    member x.ColumnRule
        with get() = x.GetPropertyValue("column-rule")
        and set v  = x.SetProperty("column-rule", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-rule-color
    member x.ColumnRuleColor
        with get() = x.GetPropertyValue("column-rule-color")
        and set v  = x.SetProperty("column-rule-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-rule-style
    member x.ColumnRuleStyle
        with get() = x.GetPropertyValue("column-rule-style")
        and set v  = x.SetProperty("column-rule-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-rule-width
    member x.ColumnRuleWidth
        with get() = x.GetPropertyValue("column-rule-width")
        and set v  = x.SetProperty("column-rule-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-span
    member x.ColumnSpan
        with get() = x.GetPropertyValue("column-span")
        and set v  = x.SetProperty("column-span", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-width
    member x.ColumnWidth
        with get() = x.GetPropertyValue("column-width")
        and set v  = x.SetProperty("column-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/columns
    member x.Columns
        with get() = x.GetPropertyValue("columns")
        and set v  = x.SetProperty("columns", v)

    /// https://developer.mozilla.org/docs/Web/CSS/contain
    member x.Contain
        with get() = x.GetPropertyValue("contain")
        and set v  = x.SetProperty("contain", v)

    /// https://developer.mozilla.org/docs/Web/CSS/content
    member x.Content
        with get() = x.GetPropertyValue("content")
        and set v  = x.SetProperty("content", v)

    /// https://developer.mozilla.org/docs/Web/CSS/counter-increment
    member x.CounterIncrement
        with get() = x.GetPropertyValue("counter-increment")
        and set v  = x.SetProperty("counter-increment", v)

    /// https://developer.mozilla.org/docs/Web/CSS/counter-reset
    member x.CounterReset
        with get() = x.GetPropertyValue("counter-reset")
        and set v  = x.SetProperty("counter-reset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/counter-set
    member x.CounterSet
        with get() = x.GetPropertyValue("counter-set")
        and set v  = x.SetProperty("counter-set", v)

    /// https://developer.mozilla.org/docs/Web/CSS/cursor
    member x.Cursor
        with get() = x.GetPropertyValue("cursor")
        and set v  = x.SetProperty("cursor", v)

    /// https://developer.mozilla.org/docs/Web/CSS/direction
    member x.Direction
        with get() = x.GetPropertyValue("direction")
        and set v  = x.SetProperty("direction", v)

    /// https://developer.mozilla.org/docs/Web/CSS/display
    member x.Display
        with get() = x.GetPropertyValue("display")
        and set v  = x.SetProperty("display", v)

    /// https://developer.mozilla.org/docs/Web/CSS/empty-cells
    member x.EmptyCells
        with get() = x.GetPropertyValue("empty-cells")
        and set v  = x.SetProperty("empty-cells", v)

    /// https://developer.mozilla.org/docs/Web/CSS/filter
    member x.Filter
        with get() = x.GetPropertyValue("filter")
        and set v  = x.SetProperty("filter", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex
    member x.Flex
        with get() = x.GetPropertyValue("flex")
        and set v  = x.SetProperty("flex", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-basis
    member x.FlexBasis
        with get() = x.GetPropertyValue("flex-basis")
        and set v  = x.SetProperty("flex-basis", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-direction
    member x.FlexDirection
        with get() = x.GetPropertyValue("flex-direction")
        and set v  = x.SetProperty("flex-direction", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-flow
    member x.FlexFlow
        with get() = x.GetPropertyValue("flex-flow")
        and set v  = x.SetProperty("flex-flow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-grow
    member x.FlexGrow
        with get() = x.GetPropertyValue("flex-grow")
        and set v  = x.SetProperty("flex-grow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-shrink
    member x.FlexShrink
        with get() = x.GetPropertyValue("flex-shrink")
        and set v  = x.SetProperty("flex-shrink", v)

    /// https://developer.mozilla.org/docs/Web/CSS/flex-wrap
    member x.FlexWrap
        with get() = x.GetPropertyValue("flex-wrap")
        and set v  = x.SetProperty("flex-wrap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/float
    member x.Float
        with get() = x.GetPropertyValue("float")
        and set v  = x.SetProperty("float", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font
    member x.Font
        with get() = x.GetPropertyValue("font")
        and set v  = x.SetProperty("font", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-family
    member x.FontFamily
        with get() = x.GetPropertyValue("font-family")
        and set v  = x.SetProperty("font-family", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-feature-settings
    member x.FontFeatureSettings
        with get() = x.GetPropertyValue("font-feature-settings")
        and set v  = x.SetProperty("font-feature-settings", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-kerning
    member x.FontKerning
        with get() = x.GetPropertyValue("font-kerning")
        and set v  = x.SetProperty("font-kerning", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-language-override
    member x.FontLanguageOverride
        with get() = x.GetPropertyValue("font-language-override")
        and set v  = x.SetProperty("font-language-override", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-optical-sizing
    member x.FontOpticalSizing
        with get() = x.GetPropertyValue("font-optical-sizing")
        and set v  = x.SetProperty("font-optical-sizing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variation-settings
    member x.FontVariationSettings
        with get() = x.GetPropertyValue("font-variation-settings")
        and set v  = x.SetProperty("font-variation-settings", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-size
    member x.FontSize
        with get() = x.GetPropertyValue("font-size")
        and set v  = x.SetProperty("font-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-size-adjust
    member x.FontSizeAdjust
        with get() = x.GetPropertyValue("font-size-adjust")
        and set v  = x.SetProperty("font-size-adjust", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-stretch
    member x.FontStretch
        with get() = x.GetPropertyValue("font-stretch")
        and set v  = x.SetProperty("font-stretch", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-style
    member x.FontStyle
        with get() = x.GetPropertyValue("font-style")
        and set v  = x.SetProperty("font-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-synthesis
    member x.FontSynthesis
        with get() = x.GetPropertyValue("font-synthesis")
        and set v  = x.SetProperty("font-synthesis", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant
    member x.FontVariant
        with get() = x.GetPropertyValue("font-variant")
        and set v  = x.SetProperty("font-variant", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-alternates
    member x.FontVariantAlternates
        with get() = x.GetPropertyValue("font-variant-alternates")
        and set v  = x.SetProperty("font-variant-alternates", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-caps
    member x.FontVariantCaps
        with get() = x.GetPropertyValue("font-variant-caps")
        and set v  = x.SetProperty("font-variant-caps", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-east-asian
    member x.FontVariantEastAsian
        with get() = x.GetPropertyValue("font-variant-east-asian")
        and set v  = x.SetProperty("font-variant-east-asian", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-ligatures
    member x.FontVariantLigatures
        with get() = x.GetPropertyValue("font-variant-ligatures")
        and set v  = x.SetProperty("font-variant-ligatures", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-numeric
    member x.FontVariantNumeric
        with get() = x.GetPropertyValue("font-variant-numeric")
        and set v  = x.SetProperty("font-variant-numeric", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-variant-position
    member x.FontVariantPosition
        with get() = x.GetPropertyValue("font-variant-position")
        and set v  = x.SetProperty("font-variant-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/font-weight
    member x.FontWeight
        with get() = x.GetPropertyValue("font-weight")
        and set v  = x.SetProperty("font-weight", v)

    /// https://developer.mozilla.org/docs/Web/CSS/gap
    member x.Gap
        with get() = x.GetPropertyValue("gap")
        and set v  = x.SetProperty("gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid
    member x.Grid
        with get() = x.GetPropertyValue("grid")
        and set v  = x.SetProperty("grid", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-area
    member x.GridArea
        with get() = x.GetPropertyValue("grid-area")
        and set v  = x.SetProperty("grid-area", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-auto-columns
    member x.GridAutoColumns
        with get() = x.GetPropertyValue("grid-auto-columns")
        and set v  = x.SetProperty("grid-auto-columns", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-auto-flow
    member x.GridAutoFlow
        with get() = x.GetPropertyValue("grid-auto-flow")
        and set v  = x.SetProperty("grid-auto-flow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-auto-rows
    member x.GridAutoRows
        with get() = x.GetPropertyValue("grid-auto-rows")
        and set v  = x.SetProperty("grid-auto-rows", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-column
    member x.GridColumn
        with get() = x.GetPropertyValue("grid-column")
        and set v  = x.SetProperty("grid-column", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-column-end
    member x.GridColumnEnd
        with get() = x.GetPropertyValue("grid-column-end")
        and set v  = x.SetProperty("grid-column-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/column-gap
    member x.GridColumnGap
        with get() = x.GetPropertyValue("grid-column-gap")
        and set v  = x.SetProperty("grid-column-gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-column-start
    member x.GridColumnStart
        with get() = x.GetPropertyValue("grid-column-start")
        and set v  = x.SetProperty("grid-column-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/gap
    member x.GridGap
        with get() = x.GetPropertyValue("grid-gap")
        and set v  = x.SetProperty("grid-gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-row
    member x.GridRow
        with get() = x.GetPropertyValue("grid-row")
        and set v  = x.SetProperty("grid-row", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-row-end
    member x.GridRowEnd
        with get() = x.GetPropertyValue("grid-row-end")
        and set v  = x.SetProperty("grid-row-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/row-gap
    member x.GridRowGap
        with get() = x.GetPropertyValue("grid-row-gap")
        and set v  = x.SetProperty("grid-row-gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-row-start
    member x.GridRowStart
        with get() = x.GetPropertyValue("grid-row-start")
        and set v  = x.SetProperty("grid-row-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-template
    member x.GridTemplate
        with get() = x.GetPropertyValue("grid-template")
        and set v  = x.SetProperty("grid-template", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-template-areas
    member x.GridTemplateAreas
        with get() = x.GetPropertyValue("grid-template-areas")
        and set v  = x.SetProperty("grid-template-areas", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-template-columns
    member x.GridTemplateColumns
        with get() = x.GetPropertyValue("grid-template-columns")
        and set v  = x.SetProperty("grid-template-columns", v)

    /// https://developer.mozilla.org/docs/Web/CSS/grid-template-rows
    member x.GridTemplateRows
        with get() = x.GetPropertyValue("grid-template-rows")
        and set v  = x.SetProperty("grid-template-rows", v)

    /// https://developer.mozilla.org/docs/Web/CSS/hanging-punctuation
    member x.HangingPunctuation
        with get() = x.GetPropertyValue("hanging-punctuation")
        and set v  = x.SetProperty("hanging-punctuation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/height
    member x.Height
        with get() = x.GetPropertyValue("height")
        and set v  = x.SetProperty("height", v)

    /// https://developer.mozilla.org/docs/Web/CSS/hyphens
    member x.Hyphens
        with get() = x.GetPropertyValue("hyphens")
        and set v  = x.SetProperty("hyphens", v)

    /// https://developer.mozilla.org/docs/Web/CSS/image-orientation
    member x.ImageOrientation
        with get() = x.GetPropertyValue("image-orientation")
        and set v  = x.SetProperty("image-orientation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/image-rendering
    member x.ImageRendering
        with get() = x.GetPropertyValue("image-rendering")
        and set v  = x.SetProperty("image-rendering", v)

    member x.ImageResolution
        with get() = x.GetPropertyValue("image-resolution")
        and set v  = x.SetProperty("image-resolution", v)

    /// https://developer.mozilla.org/docs/Web/CSS/ime-mode
    member x.ImeMode
        with get() = x.GetPropertyValue("ime-mode")
        and set v  = x.SetProperty("ime-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/initial-letter
    member x.InitialLetter
        with get() = x.GetPropertyValue("initial-letter")
        and set v  = x.SetProperty("initial-letter", v)

    /// https://developer.mozilla.org/docs/Web/CSS/initial-letter-align
    member x.InitialLetterAlign
        with get() = x.GetPropertyValue("initial-letter-align")
        and set v  = x.SetProperty("initial-letter-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inline-size
    member x.InlineSize
        with get() = x.GetPropertyValue("inline-size")
        and set v  = x.SetProperty("inline-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset
    member x.Inset
        with get() = x.GetPropertyValue("inset")
        and set v  = x.SetProperty("inset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-block
    member x.InsetBlock
        with get() = x.GetPropertyValue("inset-block")
        and set v  = x.SetProperty("inset-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-block-end
    member x.InsetBlockEnd
        with get() = x.GetPropertyValue("inset-block-end")
        and set v  = x.SetProperty("inset-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-block-start
    member x.InsetBlockStart
        with get() = x.GetPropertyValue("inset-block-start")
        and set v  = x.SetProperty("inset-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-inline
    member x.InsetInline
        with get() = x.GetPropertyValue("inset-inline")
        and set v  = x.SetProperty("inset-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-inline-end
    member x.InsetInlineEnd
        with get() = x.GetPropertyValue("inset-inline-end")
        and set v  = x.SetProperty("inset-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/inset-inline-start
    member x.InsetInlineStart
        with get() = x.GetPropertyValue("inset-inline-start")
        and set v  = x.SetProperty("inset-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/isolation
    member x.Isolation
        with get() = x.GetPropertyValue("isolation")
        and set v  = x.SetProperty("isolation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/justify-content
    member x.JustifyContent
        with get() = x.GetPropertyValue("justify-content")
        and set v  = x.SetProperty("justify-content", v)

    /// https://developer.mozilla.org/docs/Web/CSS/justify-items
    member x.JustifyItems
        with get() = x.GetPropertyValue("justify-items")
        and set v  = x.SetProperty("justify-items", v)

    /// https://developer.mozilla.org/docs/Web/CSS/justify-self
    member x.JustifySelf
        with get() = x.GetPropertyValue("justify-self")
        and set v  = x.SetProperty("justify-self", v)

    /// https://developer.mozilla.org/docs/Web/CSS/left
    member x.Left
        with get() = x.GetPropertyValue("left")
        and set v  = x.SetProperty("left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/letter-spacing
    member x.LetterSpacing
        with get() = x.GetPropertyValue("letter-spacing")
        and set v  = x.SetProperty("letter-spacing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/line-break
    member x.LineBreak
        with get() = x.GetPropertyValue("line-break")
        and set v  = x.SetProperty("line-break", v)

    member x.LineClamp
        with get() = x.GetPropertyValue("line-clamp")
        and set v  = x.SetProperty("line-clamp", v)

    /// https://developer.mozilla.org/docs/Web/CSS/line-height
    member x.LineHeight
        with get() = x.GetPropertyValue("line-height")
        and set v  = x.SetProperty("line-height", v)

    /// https://developer.mozilla.org/docs/Web/CSS/line-height-step
    member x.LineHeightStep
        with get() = x.GetPropertyValue("line-height-step")
        and set v  = x.SetProperty("line-height-step", v)

    /// https://developer.mozilla.org/docs/Web/CSS/list-style
    member x.ListStyle
        with get() = x.GetPropertyValue("list-style")
        and set v  = x.SetProperty("list-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/list-style-image
    member x.ListStyleImage
        with get() = x.GetPropertyValue("list-style-image")
        and set v  = x.SetProperty("list-style-image", v)

    /// https://developer.mozilla.org/docs/Web/CSS/list-style-position
    member x.ListStylePosition
        with get() = x.GetPropertyValue("list-style-position")
        and set v  = x.SetProperty("list-style-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/list-style-type
    member x.ListStyleType
        with get() = x.GetPropertyValue("list-style-type")
        and set v  = x.SetProperty("list-style-type", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin
    member x.Margin
        with get() = x.GetPropertyValue("margin")
        and set v  = x.SetProperty("margin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-block
    member x.MarginBlock
        with get() = x.GetPropertyValue("margin-block")
        and set v  = x.SetProperty("margin-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-block-end
    member x.MarginBlockEnd
        with get() = x.GetPropertyValue("margin-block-end")
        and set v  = x.SetProperty("margin-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-block-start
    member x.MarginBlockStart
        with get() = x.GetPropertyValue("margin-block-start")
        and set v  = x.SetProperty("margin-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-bottom
    member x.MarginBottom
        with get() = x.GetPropertyValue("margin-bottom")
        and set v  = x.SetProperty("margin-bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-inline
    member x.MarginInline
        with get() = x.GetPropertyValue("margin-inline")
        and set v  = x.SetProperty("margin-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-inline-end
    member x.MarginInlineEnd
        with get() = x.GetPropertyValue("margin-inline-end")
        and set v  = x.SetProperty("margin-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-inline-start
    member x.MarginInlineStart
        with get() = x.GetPropertyValue("margin-inline-start")
        and set v  = x.SetProperty("margin-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-left
    member x.MarginLeft
        with get() = x.GetPropertyValue("margin-left")
        and set v  = x.SetProperty("margin-left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-right
    member x.MarginRight
        with get() = x.GetPropertyValue("margin-right")
        and set v  = x.SetProperty("margin-right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-top
    member x.MarginTop
        with get() = x.GetPropertyValue("margin-top")
        and set v  = x.SetProperty("margin-top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/margin-trim
    member x.MarginTrim
        with get() = x.GetPropertyValue("margin-trim")
        and set v  = x.SetProperty("margin-trim", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask
    member x.Mask
        with get() = x.GetPropertyValue("mask")
        and set v  = x.SetProperty("mask", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border
    member x.MaskBorder
        with get() = x.GetPropertyValue("mask-border")
        and set v  = x.SetProperty("mask-border", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-mode
    member x.MaskBorderMode
        with get() = x.GetPropertyValue("mask-border-mode")
        and set v  = x.SetProperty("mask-border-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-outset
    member x.MaskBorderOutset
        with get() = x.GetPropertyValue("mask-border-outset")
        and set v  = x.SetProperty("mask-border-outset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-repeat
    member x.MaskBorderRepeat
        with get() = x.GetPropertyValue("mask-border-repeat")
        and set v  = x.SetProperty("mask-border-repeat", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-slice
    member x.MaskBorderSlice
        with get() = x.GetPropertyValue("mask-border-slice")
        and set v  = x.SetProperty("mask-border-slice", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-source
    member x.MaskBorderSource
        with get() = x.GetPropertyValue("mask-border-source")
        and set v  = x.SetProperty("mask-border-source", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-border-width
    member x.MaskBorderWidth
        with get() = x.GetPropertyValue("mask-border-width")
        and set v  = x.SetProperty("mask-border-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-clip
    member x.MaskClip
        with get() = x.GetPropertyValue("mask-clip")
        and set v  = x.SetProperty("mask-clip", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-composite
    member x.MaskComposite
        with get() = x.GetPropertyValue("mask-composite")
        and set v  = x.SetProperty("mask-composite", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-image
    member x.MaskImage
        with get() = x.GetPropertyValue("mask-image")
        and set v  = x.SetProperty("mask-image", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-mode
    member x.MaskMode
        with get() = x.GetPropertyValue("mask-mode")
        and set v  = x.SetProperty("mask-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-origin
    member x.MaskOrigin
        with get() = x.GetPropertyValue("mask-origin")
        and set v  = x.SetProperty("mask-origin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-position
    member x.MaskPosition
        with get() = x.GetPropertyValue("mask-position")
        and set v  = x.SetProperty("mask-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-repeat
    member x.MaskRepeat
        with get() = x.GetPropertyValue("mask-repeat")
        and set v  = x.SetProperty("mask-repeat", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-size
    member x.MaskSize
        with get() = x.GetPropertyValue("mask-size")
        and set v  = x.SetProperty("mask-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mask-type
    member x.MaskType
        with get() = x.GetPropertyValue("mask-type")
        and set v  = x.SetProperty("mask-type", v)

    /// https://developer.mozilla.org/docs/Web/CSS/max-block-size
    member x.MaxBlockSize
        with get() = x.GetPropertyValue("max-block-size")
        and set v  = x.SetProperty("max-block-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/max-height
    member x.MaxHeight
        with get() = x.GetPropertyValue("max-height")
        and set v  = x.SetProperty("max-height", v)

    /// https://developer.mozilla.org/docs/Web/CSS/max-inline-size
    member x.MaxInlineSize
        with get() = x.GetPropertyValue("max-inline-size")
        and set v  = x.SetProperty("max-inline-size", v)

    member x.MaxLines
        with get() = x.GetPropertyValue("max-lines")
        and set v  = x.SetProperty("max-lines", v)

    /// https://developer.mozilla.org/docs/Web/CSS/max-width
    member x.MaxWidth
        with get() = x.GetPropertyValue("max-width")
        and set v  = x.SetProperty("max-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/min-block-size
    member x.MinBlockSize
        with get() = x.GetPropertyValue("min-block-size")
        and set v  = x.SetProperty("min-block-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/min-height
    member x.MinHeight
        with get() = x.GetPropertyValue("min-height")
        and set v  = x.SetProperty("min-height", v)

    /// https://developer.mozilla.org/docs/Web/CSS/min-inline-size
    member x.MinInlineSize
        with get() = x.GetPropertyValue("min-inline-size")
        and set v  = x.SetProperty("min-inline-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/min-width
    member x.MinWidth
        with get() = x.GetPropertyValue("min-width")
        and set v  = x.SetProperty("min-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/mix-blend-mode
    member x.MixBlendMode
        with get() = x.GetPropertyValue("mix-blend-mode")
        and set v  = x.SetProperty("mix-blend-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/object-fit
    member x.ObjectFit
        with get() = x.GetPropertyValue("object-fit")
        and set v  = x.SetProperty("object-fit", v)

    /// https://developer.mozilla.org/docs/Web/CSS/object-position
    member x.ObjectPosition
        with get() = x.GetPropertyValue("object-position")
        and set v  = x.SetProperty("object-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/offset
    member x.Offset
        with get() = x.GetPropertyValue("offset")
        and set v  = x.SetProperty("offset", v)

    member x.OffsetAnchor
        with get() = x.GetPropertyValue("offset-anchor")
        and set v  = x.SetProperty("offset-anchor", v)

    /// https://developer.mozilla.org/docs/Web/CSS/offset-distance
    member x.OffsetDistance
        with get() = x.GetPropertyValue("offset-distance")
        and set v  = x.SetProperty("offset-distance", v)

    /// https://developer.mozilla.org/docs/Web/CSS/offset-path
    member x.OffsetPath
        with get() = x.GetPropertyValue("offset-path")
        and set v  = x.SetProperty("offset-path", v)

    member x.OffsetPosition
        with get() = x.GetPropertyValue("offset-position")
        and set v  = x.SetProperty("offset-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/offset-rotate
    member x.OffsetRotate
        with get() = x.GetPropertyValue("offset-rotate")
        and set v  = x.SetProperty("offset-rotate", v)

    /// https://developer.mozilla.org/docs/Web/CSS/opacity
    member x.Opacity
        with get() = x.GetPropertyValue("opacity")
        and set v  = x.SetProperty("opacity", v)

    /// https://developer.mozilla.org/docs/Web/CSS/order
    member x.Order
        with get() = x.GetPropertyValue("order")
        and set v  = x.SetProperty("order", v)

    /// https://developer.mozilla.org/docs/Web/CSS/orphans
    member x.Orphans
        with get() = x.GetPropertyValue("orphans")
        and set v  = x.SetProperty("orphans", v)

    /// https://developer.mozilla.org/docs/Web/CSS/outline
    member x.Outline
        with get() = x.GetPropertyValue("outline")
        and set v  = x.SetProperty("outline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/outline-color
    member x.OutlineColor
        with get() = x.GetPropertyValue("outline-color")
        and set v  = x.SetProperty("outline-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/outline-offset
    member x.OutlineOffset
        with get() = x.GetPropertyValue("outline-offset")
        and set v  = x.SetProperty("outline-offset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/outline-style
    member x.OutlineStyle
        with get() = x.GetPropertyValue("outline-style")
        and set v  = x.SetProperty("outline-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/outline-width
    member x.OutlineWidth
        with get() = x.GetPropertyValue("outline-width")
        and set v  = x.SetProperty("outline-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overflow
    member x.Overflow
        with get() = x.GetPropertyValue("overflow")
        and set v  = x.SetProperty("overflow", v)

    member x.OverflowAnchor
        with get() = x.GetPropertyValue("overflow-anchor")
        and set v  = x.SetProperty("overflow-anchor", v)

    member x.OverflowBlock
        with get() = x.GetPropertyValue("overflow-block")
        and set v  = x.SetProperty("overflow-block", v)

    /// https://developer.mozilla.org/docs/Mozilla/CSS/overflow-clip-box
    member x.OverflowClipBox
        with get() = x.GetPropertyValue("overflow-clip-box")
        and set v  = x.SetProperty("overflow-clip-box", v)

    member x.OverflowInline
        with get() = x.GetPropertyValue("overflow-inline")
        and set v  = x.SetProperty("overflow-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overflow-wrap
    member x.OverflowWrap
        with get() = x.GetPropertyValue("overflow-wrap")
        and set v  = x.SetProperty("overflow-wrap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overflow-x
    member x.OverflowX
        with get() = x.GetPropertyValue("overflow-x")
        and set v  = x.SetProperty("overflow-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overflow-y
    member x.OverflowY
        with get() = x.GetPropertyValue("overflow-y")
        and set v  = x.SetProperty("overflow-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overscroll-behavior
    member x.OverscrollBehavior
        with get() = x.GetPropertyValue("overscroll-behavior")
        and set v  = x.SetProperty("overscroll-behavior", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overscroll-behavior-block
    member x.OverscrollBehaviorBlock
        with get() = x.GetPropertyValue("overscroll-behavior-block")
        and set v  = x.SetProperty("overscroll-behavior-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overscroll-behavior-inline
    member x.OverscrollBehaviorInline
        with get() = x.GetPropertyValue("overscroll-behavior-inline")
        and set v  = x.SetProperty("overscroll-behavior-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overscroll-behavior-x
    member x.OverscrollBehaviorX
        with get() = x.GetPropertyValue("overscroll-behavior-x")
        and set v  = x.SetProperty("overscroll-behavior-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overscroll-behavior-y
    member x.OverscrollBehaviorY
        with get() = x.GetPropertyValue("overscroll-behavior-y")
        and set v  = x.SetProperty("overscroll-behavior-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding
    member x.Padding
        with get() = x.GetPropertyValue("padding")
        and set v  = x.SetProperty("padding", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-block
    member x.PaddingBlock
        with get() = x.GetPropertyValue("padding-block")
        and set v  = x.SetProperty("padding-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-block-end
    member x.PaddingBlockEnd
        with get() = x.GetPropertyValue("padding-block-end")
        and set v  = x.SetProperty("padding-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-block-start
    member x.PaddingBlockStart
        with get() = x.GetPropertyValue("padding-block-start")
        and set v  = x.SetProperty("padding-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-bottom
    member x.PaddingBottom
        with get() = x.GetPropertyValue("padding-bottom")
        and set v  = x.SetProperty("padding-bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-inline
    member x.PaddingInline
        with get() = x.GetPropertyValue("padding-inline")
        and set v  = x.SetProperty("padding-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-inline-end
    member x.PaddingInlineEnd
        with get() = x.GetPropertyValue("padding-inline-end")
        and set v  = x.SetProperty("padding-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-inline-start
    member x.PaddingInlineStart
        with get() = x.GetPropertyValue("padding-inline-start")
        and set v  = x.SetProperty("padding-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-left
    member x.PaddingLeft
        with get() = x.GetPropertyValue("padding-left")
        and set v  = x.SetProperty("padding-left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-right
    member x.PaddingRight
        with get() = x.GetPropertyValue("padding-right")
        and set v  = x.SetProperty("padding-right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/padding-top
    member x.PaddingTop
        with get() = x.GetPropertyValue("padding-top")
        and set v  = x.SetProperty("padding-top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/page-break-after
    member x.PageBreakAfter
        with get() = x.GetPropertyValue("page-break-after")
        and set v  = x.SetProperty("page-break-after", v)

    /// https://developer.mozilla.org/docs/Web/CSS/page-break-before
    member x.PageBreakBefore
        with get() = x.GetPropertyValue("page-break-before")
        and set v  = x.SetProperty("page-break-before", v)

    /// https://developer.mozilla.org/docs/Web/CSS/page-break-inside
    member x.PageBreakInside
        with get() = x.GetPropertyValue("page-break-inside")
        and set v  = x.SetProperty("page-break-inside", v)

    /// https://developer.mozilla.org/docs/Web/CSS/paint-order
    member x.PaintOrder
        with get() = x.GetPropertyValue("paint-order")
        and set v  = x.SetProperty("paint-order", v)

    /// https://developer.mozilla.org/docs/Web/CSS/perspective
    member x.Perspective
        with get() = x.GetPropertyValue("perspective")
        and set v  = x.SetProperty("perspective", v)

    /// https://developer.mozilla.org/docs/Web/CSS/perspective-origin
    member x.PerspectiveOrigin
        with get() = x.GetPropertyValue("perspective-origin")
        and set v  = x.SetProperty("perspective-origin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/place-content
    member x.PlaceContent
        with get() = x.GetPropertyValue("place-content")
        and set v  = x.SetProperty("place-content", v)

    /// https://developer.mozilla.org/docs/Web/CSS/place-items
    member x.PlaceItems
        with get() = x.GetPropertyValue("place-items")
        and set v  = x.SetProperty("place-items", v)

    /// https://developer.mozilla.org/docs/Web/CSS/place-self
    member x.PlaceSelf
        with get() = x.GetPropertyValue("place-self")
        and set v  = x.SetProperty("place-self", v)

    /// https://developer.mozilla.org/docs/Web/CSS/pointer-events
    member x.PointerEvents
        with get() = x.GetPropertyValue("pointer-events")
        and set v  = x.SetProperty("pointer-events", v)

    /// https://developer.mozilla.org/docs/Web/CSS/position
    member x.Position
        with get() = x.GetPropertyValue("position")
        and set v  = x.SetProperty("position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/quotes
    member x.Quotes
        with get() = x.GetPropertyValue("quotes")
        and set v  = x.SetProperty("quotes", v)

    /// https://developer.mozilla.org/docs/Web/CSS/resize
    member x.Resize
        with get() = x.GetPropertyValue("resize")
        and set v  = x.SetProperty("resize", v)

    /// https://developer.mozilla.org/docs/Web/CSS/right
    member x.Right
        with get() = x.GetPropertyValue("right")
        and set v  = x.SetProperty("right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/rotate
    member x.Rotate
        with get() = x.GetPropertyValue("rotate")
        and set v  = x.SetProperty("rotate", v)

    /// https://developer.mozilla.org/docs/Web/CSS/row-gap
    member x.RowGap
        with get() = x.GetPropertyValue("row-gap")
        and set v  = x.SetProperty("row-gap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/ruby-align
    member x.RubyAlign
        with get() = x.GetPropertyValue("ruby-align")
        and set v  = x.SetProperty("ruby-align", v)

    member x.RubyMerge
        with get() = x.GetPropertyValue("ruby-merge")
        and set v  = x.SetProperty("ruby-merge", v)

    /// https://developer.mozilla.org/docs/Web/CSS/ruby-position
    member x.RubyPosition
        with get() = x.GetPropertyValue("ruby-position")
        and set v  = x.SetProperty("ruby-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scale
    member x.Scale
        with get() = x.GetPropertyValue("scale")
        and set v  = x.SetProperty("scale", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scrollbar-color
    member x.ScrollbarColor
        with get() = x.GetPropertyValue("scrollbar-color")
        and set v  = x.SetProperty("scrollbar-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scrollbar-width
    member x.ScrollbarWidth
        with get() = x.GetPropertyValue("scrollbar-width")
        and set v  = x.SetProperty("scrollbar-width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-behavior
    member x.ScrollBehavior
        with get() = x.GetPropertyValue("scroll-behavior")
        and set v  = x.SetProperty("scroll-behavior", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin
    member x.ScrollMargin
        with get() = x.GetPropertyValue("scroll-margin")
        and set v  = x.SetProperty("scroll-margin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-block
    member x.ScrollMarginBlock
        with get() = x.GetPropertyValue("scroll-margin-block")
        and set v  = x.SetProperty("scroll-margin-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-block-start
    member x.ScrollMarginBlockStart
        with get() = x.GetPropertyValue("scroll-margin-block-start")
        and set v  = x.SetProperty("scroll-margin-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-block-end
    member x.ScrollMarginBlockEnd
        with get() = x.GetPropertyValue("scroll-margin-block-end")
        and set v  = x.SetProperty("scroll-margin-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-bottom
    member x.ScrollMarginBottom
        with get() = x.GetPropertyValue("scroll-margin-bottom")
        and set v  = x.SetProperty("scroll-margin-bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-inline
    member x.ScrollMarginInline
        with get() = x.GetPropertyValue("scroll-margin-inline")
        and set v  = x.SetProperty("scroll-margin-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-inline-start
    member x.ScrollMarginInlineStart
        with get() = x.GetPropertyValue("scroll-margin-inline-start")
        and set v  = x.SetProperty("scroll-margin-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-inline-end
    member x.ScrollMarginInlineEnd
        with get() = x.GetPropertyValue("scroll-margin-inline-end")
        and set v  = x.SetProperty("scroll-margin-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-left
    member x.ScrollMarginLeft
        with get() = x.GetPropertyValue("scroll-margin-left")
        and set v  = x.SetProperty("scroll-margin-left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-right
    member x.ScrollMarginRight
        with get() = x.GetPropertyValue("scroll-margin-right")
        and set v  = x.SetProperty("scroll-margin-right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-margin-top
    member x.ScrollMarginTop
        with get() = x.GetPropertyValue("scroll-margin-top")
        and set v  = x.SetProperty("scroll-margin-top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding
    member x.ScrollPadding
        with get() = x.GetPropertyValue("scroll-padding")
        and set v  = x.SetProperty("scroll-padding", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-block
    member x.ScrollPaddingBlock
        with get() = x.GetPropertyValue("scroll-padding-block")
        and set v  = x.SetProperty("scroll-padding-block", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-block-start
    member x.ScrollPaddingBlockStart
        with get() = x.GetPropertyValue("scroll-padding-block-start")
        and set v  = x.SetProperty("scroll-padding-block-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-block-end
    member x.ScrollPaddingBlockEnd
        with get() = x.GetPropertyValue("scroll-padding-block-end")
        and set v  = x.SetProperty("scroll-padding-block-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-bottom
    member x.ScrollPaddingBottom
        with get() = x.GetPropertyValue("scroll-padding-bottom")
        and set v  = x.SetProperty("scroll-padding-bottom", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-inline
    member x.ScrollPaddingInline
        with get() = x.GetPropertyValue("scroll-padding-inline")
        and set v  = x.SetProperty("scroll-padding-inline", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-inline-start
    member x.ScrollPaddingInlineStart
        with get() = x.GetPropertyValue("scroll-padding-inline-start")
        and set v  = x.SetProperty("scroll-padding-inline-start", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-inline-end
    member x.ScrollPaddingInlineEnd
        with get() = x.GetPropertyValue("scroll-padding-inline-end")
        and set v  = x.SetProperty("scroll-padding-inline-end", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-left
    member x.ScrollPaddingLeft
        with get() = x.GetPropertyValue("scroll-padding-left")
        and set v  = x.SetProperty("scroll-padding-left", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-right
    member x.ScrollPaddingRight
        with get() = x.GetPropertyValue("scroll-padding-right")
        and set v  = x.SetProperty("scroll-padding-right", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-padding-top
    member x.ScrollPaddingTop
        with get() = x.GetPropertyValue("scroll-padding-top")
        and set v  = x.SetProperty("scroll-padding-top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-align
    member x.ScrollSnapAlign
        with get() = x.GetPropertyValue("scroll-snap-align")
        and set v  = x.SetProperty("scroll-snap-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-coordinate
    member x.ScrollSnapCoordinate
        with get() = x.GetPropertyValue("scroll-snap-coordinate")
        and set v  = x.SetProperty("scroll-snap-coordinate", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-destination
    member x.ScrollSnapDestination
        with get() = x.GetPropertyValue("scroll-snap-destination")
        and set v  = x.SetProperty("scroll-snap-destination", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-points-x
    member x.ScrollSnapPointsX
        with get() = x.GetPropertyValue("scroll-snap-points-x")
        and set v  = x.SetProperty("scroll-snap-points-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-points-y
    member x.ScrollSnapPointsY
        with get() = x.GetPropertyValue("scroll-snap-points-y")
        and set v  = x.SetProperty("scroll-snap-points-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-stop
    member x.ScrollSnapStop
        with get() = x.GetPropertyValue("scroll-snap-stop")
        and set v  = x.SetProperty("scroll-snap-stop", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-type
    member x.ScrollSnapType
        with get() = x.GetPropertyValue("scroll-snap-type")
        and set v  = x.SetProperty("scroll-snap-type", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-type-x
    member x.ScrollSnapTypeX
        with get() = x.GetPropertyValue("scroll-snap-type-x")
        and set v  = x.SetProperty("scroll-snap-type-x", v)

    /// https://developer.mozilla.org/docs/Web/CSS/scroll-snap-type-y
    member x.ScrollSnapTypeY
        with get() = x.GetPropertyValue("scroll-snap-type-y")
        and set v  = x.SetProperty("scroll-snap-type-y", v)

    /// https://developer.mozilla.org/docs/Web/CSS/shape-image-threshold
    member x.ShapeImageThreshold
        with get() = x.GetPropertyValue("shape-image-threshold")
        and set v  = x.SetProperty("shape-image-threshold", v)

    /// https://developer.mozilla.org/docs/Web/CSS/shape-margin
    member x.ShapeMargin
        with get() = x.GetPropertyValue("shape-margin")
        and set v  = x.SetProperty("shape-margin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/shape-outside
    member x.ShapeOutside
        with get() = x.GetPropertyValue("shape-outside")
        and set v  = x.SetProperty("shape-outside", v)

    /// https://developer.mozilla.org/docs/Web/CSS/tab-size
    member x.TabSize
        with get() = x.GetPropertyValue("tab-size")
        and set v  = x.SetProperty("tab-size", v)

    /// https://developer.mozilla.org/docs/Web/CSS/table-layout
    member x.TableLayout
        with get() = x.GetPropertyValue("table-layout")
        and set v  = x.SetProperty("table-layout", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-align
    member x.TextAlign
        with get() = x.GetPropertyValue("text-align")
        and set v  = x.SetProperty("text-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-align-last
    member x.TextAlignLast
        with get() = x.GetPropertyValue("text-align-last")
        and set v  = x.SetProperty("text-align-last", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-combine-upright
    member x.TextCombineUpright
        with get() = x.GetPropertyValue("text-combine-upright")
        and set v  = x.SetProperty("text-combine-upright", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration
    member x.TextDecoration
        with get() = x.GetPropertyValue("text-decoration")
        and set v  = x.SetProperty("text-decoration", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-color
    member x.TextDecorationColor
        with get() = x.GetPropertyValue("text-decoration-color")
        and set v  = x.SetProperty("text-decoration-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-line
    member x.TextDecorationLine
        with get() = x.GetPropertyValue("text-decoration-line")
        and set v  = x.SetProperty("text-decoration-line", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-skip
    member x.TextDecorationSkip
        with get() = x.GetPropertyValue("text-decoration-skip")
        and set v  = x.SetProperty("text-decoration-skip", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-skip-ink
    member x.TextDecorationSkipInk
        with get() = x.GetPropertyValue("text-decoration-skip-ink")
        and set v  = x.SetProperty("text-decoration-skip-ink", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-style
    member x.TextDecorationStyle
        with get() = x.GetPropertyValue("text-decoration-style")
        and set v  = x.SetProperty("text-decoration-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-decoration-thickness
    member x.TextDecorationThickness
        with get() = x.GetPropertyValue("text-decoration-thickness")
        and set v  = x.SetProperty("text-decoration-thickness", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-emphasis
    member x.TextEmphasis
        with get() = x.GetPropertyValue("text-emphasis")
        and set v  = x.SetProperty("text-emphasis", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-emphasis-color
    member x.TextEmphasisColor
        with get() = x.GetPropertyValue("text-emphasis-color")
        and set v  = x.SetProperty("text-emphasis-color", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-emphasis-position
    member x.TextEmphasisPosition
        with get() = x.GetPropertyValue("text-emphasis-position")
        and set v  = x.SetProperty("text-emphasis-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-emphasis-style
    member x.TextEmphasisStyle
        with get() = x.GetPropertyValue("text-emphasis-style")
        and set v  = x.SetProperty("text-emphasis-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-indent
    member x.TextIndent
        with get() = x.GetPropertyValue("text-indent")
        and set v  = x.SetProperty("text-indent", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-justify
    member x.TextJustify
        with get() = x.GetPropertyValue("text-justify")
        and set v  = x.SetProperty("text-justify", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-orientation
    member x.TextOrientation
        with get() = x.GetPropertyValue("text-orientation")
        and set v  = x.SetProperty("text-orientation", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-overflow
    member x.TextOverflow
        with get() = x.GetPropertyValue("text-overflow")
        and set v  = x.SetProperty("text-overflow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-rendering
    member x.TextRendering
        with get() = x.GetPropertyValue("text-rendering")
        and set v  = x.SetProperty("text-rendering", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-shadow
    member x.TextShadow
        with get() = x.GetPropertyValue("text-shadow")
        and set v  = x.SetProperty("text-shadow", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-size-adjust
    member x.TextSizeAdjust
        with get() = x.GetPropertyValue("text-size-adjust")
        and set v  = x.SetProperty("text-size-adjust", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-transform
    member x.TextTransform
        with get() = x.GetPropertyValue("text-transform")
        and set v  = x.SetProperty("text-transform", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-underline-offset
    member x.TextUnderlineOffset
        with get() = x.GetPropertyValue("text-underline-offset")
        and set v  = x.SetProperty("text-underline-offset", v)

    /// https://developer.mozilla.org/docs/Web/CSS/text-underline-position
    member x.TextUnderlinePosition
        with get() = x.GetPropertyValue("text-underline-position")
        and set v  = x.SetProperty("text-underline-position", v)

    /// https://developer.mozilla.org/docs/Web/CSS/top
    member x.Top
        with get() = x.GetPropertyValue("top")
        and set v  = x.SetProperty("top", v)

    /// https://developer.mozilla.org/docs/Web/CSS/touch-action
    member x.TouchAction
        with get() = x.GetPropertyValue("touch-action")
        and set v  = x.SetProperty("touch-action", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transform
    member x.Transform
        with get() = x.GetPropertyValue("transform")
        and set v  = x.SetProperty("transform", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transform-box
    member x.TransformBox
        with get() = x.GetPropertyValue("transform-box")
        and set v  = x.SetProperty("transform-box", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transform-origin
    member x.TransformOrigin
        with get() = x.GetPropertyValue("transform-origin")
        and set v  = x.SetProperty("transform-origin", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transform-style
    member x.TransformStyle
        with get() = x.GetPropertyValue("transform-style")
        and set v  = x.SetProperty("transform-style", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transition
    member x.Transition
        with get() = x.GetPropertyValue("transition")
        and set v  = x.SetProperty("transition", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transition-delay
    member x.TransitionDelay
        with get() = x.GetPropertyValue("transition-delay")
        and set v  = x.SetProperty("transition-delay", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transition-duration
    member x.TransitionDuration
        with get() = x.GetPropertyValue("transition-duration")
        and set v  = x.SetProperty("transition-duration", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transition-property
    member x.TransitionProperty
        with get() = x.GetPropertyValue("transition-property")
        and set v  = x.SetProperty("transition-property", v)

    /// https://developer.mozilla.org/docs/Web/CSS/transition-timing-function
    member x.TransitionTimingFunction
        with get() = x.GetPropertyValue("transition-timing-function")
        and set v  = x.SetProperty("transition-timing-function", v)

    /// https://developer.mozilla.org/docs/Web/CSS/translate
    member x.Translate
        with get() = x.GetPropertyValue("translate")
        and set v  = x.SetProperty("translate", v)

    /// https://developer.mozilla.org/docs/Web/CSS/unicode-bidi
    member x.UnicodeBidi
        with get() = x.GetPropertyValue("unicode-bidi")
        and set v  = x.SetProperty("unicode-bidi", v)

    /// https://developer.mozilla.org/docs/Web/CSS/user-select
    member x.UserSelect
        with get() = x.GetPropertyValue("user-select")
        and set v  = x.SetProperty("user-select", v)

    /// https://developer.mozilla.org/docs/Web/CSS/vertical-align
    member x.VerticalAlign
        with get() = x.GetPropertyValue("vertical-align")
        and set v  = x.SetProperty("vertical-align", v)

    /// https://developer.mozilla.org/docs/Web/CSS/visibility
    member x.Visibility
        with get() = x.GetPropertyValue("visibility")
        and set v  = x.SetProperty("visibility", v)

    /// https://developer.mozilla.org/docs/Web/CSS/white-space
    member x.WhiteSpace
        with get() = x.GetPropertyValue("white-space")
        and set v  = x.SetProperty("white-space", v)

    /// https://developer.mozilla.org/docs/Web/CSS/widows
    member x.Widows
        with get() = x.GetPropertyValue("widows")
        and set v  = x.SetProperty("widows", v)

    /// https://developer.mozilla.org/docs/Web/CSS/width
    member x.Width
        with get() = x.GetPropertyValue("width")
        and set v  = x.SetProperty("width", v)

    /// https://developer.mozilla.org/docs/Web/CSS/will-change
    member x.WillChange
        with get() = x.GetPropertyValue("will-change")
        and set v  = x.SetProperty("will-change", v)

    /// https://developer.mozilla.org/docs/Web/CSS/word-break
    member x.WordBreak
        with get() = x.GetPropertyValue("word-break")
        and set v  = x.SetProperty("word-break", v)

    /// https://developer.mozilla.org/docs/Web/CSS/word-spacing
    member x.WordSpacing
        with get() = x.GetPropertyValue("word-spacing")
        and set v  = x.SetProperty("word-spacing", v)

    /// https://developer.mozilla.org/docs/Web/CSS/overflow-wrap
    member x.WordWrap
        with get() = x.GetPropertyValue("word-wrap")
        and set v  = x.SetProperty("word-wrap", v)

    /// https://developer.mozilla.org/docs/Web/CSS/writing-mode
    member x.WritingMode
        with get() = x.GetPropertyValue("writing-mode")
        and set v  = x.SetProperty("writing-mode", v)

    /// https://developer.mozilla.org/docs/Web/CSS/z-index
    member x.ZIndex
        with get() = x.GetPropertyValue("z-index")
        and set v  = x.SetProperty("z-index", v)

    /// https://developer.mozilla.org/docs/Web/CSS/zoom
    member x.Zoom
        with get() = x.GetPropertyValue("zoom")
        and set v  = x.SetProperty("zoom", v)

[<AllowNullLiteral>]
type HTMLCollection(r : JSObject) =
    inherit JsObj(r)

    member x.Length : int =
        r.GetObjectProperty("length") |> convert
        
    member x.Item
        with get(i : int) : Element = r.Invoke("item", i) |> convert
        
    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = (Seq.init x.Length (fun i -> x.[i])).GetEnumerator() :> _
        
    interface System.Collections.Generic.IEnumerable<Element> with
        member x.GetEnumerator() = (Seq.init x.Length (fun i -> x.[i])).GetEnumerator()

    new (o : JsObj) = HTMLCollection o.Reference

[<AllowNullLiteral>]
type Blob(r : JSObject) =
    inherit JsObj(r)

[<AllowNullLiteral>]
type DataTransfer(r : JSObject) =
    inherit JsObj(r)
    
[<AllowNullLiteral>]
type Request(r : JSObject) =
    inherit JsObj(r)

[<AllowNullLiteral>]
type Response(r : JSObject) =
    inherit JsObj(r)
    
[<AllowNullLiteral>]
type TouchList(r : JSObject) =
    inherit JsObj(r)

[<AllowNullLiteral>]
type Gamepad(r : JSObject) =
    inherit JsObj(r)






[<AllowNullLiteral>]
type NodeList(r : JSObject) =
    inherit JsObj(r)

    member x.Length : int =
        r.GetObjectProperty("length") |> convert
        
    member x.Item
        with get(i : int) : Node = r.Invoke("item", i) |> convert
        
    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = new Iterator<Node>((fun () -> r.Invoke("values") |> unbox)) :> _
        
    interface System.Collections.Generic.IEnumerable<Node> with
        member x.GetEnumerator() = new Iterator<Node>((fun () -> r.Invoke("values") |> unbox)) :> _

    new (o : JsObj) =
        NodeList o.Reference

type TokenList(r : JSObject) =
    inherit JsObj(r)

    member x.Length = r.GetObjectProperty("length") |> unbox<int>
    member x.Value = r.GetObjectProperty("value") |> unbox<string>

    member x.Contains(element : string) = r.Invoke("contains", element) |> unbox<bool>
    member x.Add(element : string) = r.Invoke("add", element) |> ignore
    member x.Remove(element : string) = r.Invoke("remove", element) |> ignore
    member x.Replace(oldElement : string, newElement : string) = r.Invoke("replace", oldElement, newElement) |> ignore
    member x.Supports(element : string) = r.Invoke("supports", element) |> unbox<bool>
    member x.Toggle(element : string) = r.Invoke("toggle", element) |> unbox<bool>
    member x.Toggle(element : string, force : bool) = r.Invoke("toggle", element, force) |> unbox<bool>
    
    interface System.Collections.IEnumerable with
        member x.GetEnumerator() = new Iterator<string>((fun () -> r.Invoke("values") |> unbox), convert) :> _

    interface System.Collections.Generic.IEnumerable<string> with
        member x.GetEnumerator() = new Iterator<string>((fun () -> r.Invoke("values") |> unbox), convert) :> _

    member x.Item
        with get(i : int) = r.Invoke("item", i) |> unbox<string>

    new(o : JsObj) = TokenList(o.Reference)

[<AllowNullLiteral>]
type Attr(r : JSObject) =
    inherit Node(r)

    member x.Name = r.GetObjectProperty("name") |> unbox<string>
    member x.NamespaceURI = r.GetObjectProperty("namespaceURI") |> unbox<string>
    member x.LocalName = r.GetObjectProperty("localName") |> unbox<string>
    member x.Prefix = r.GetObjectProperty("prefix") |> unbox<string>
    member x.OwnerElement = 
        let o = r.GetObjectProperty("ownerElement")
        if isNull o then null
        else Element (unbox<JSObject> o)

    member x.Value = r.GetObjectProperty("value") |> net
    
    new (name : string, value : obj) =
        let d = Runtime.GetGlobalObject("document") |> unbox<JSObject>
        let att = d.Invoke("createAttribute", name) |> unbox<JSObject>
        att.SetObjectProperty("value", js value)
        Attr(att)
        
    new (ns : string, name : string, value : obj) =
        let d = Runtime.GetGlobalObject("document") |> unbox<JSObject>
        let att = d.Invoke("createAttributeNS", ns, name) |> unbox<JSObject>
        att.SetObjectProperty("value", js value)
        Attr(att)

    new (o : JsObj) =
        Attr o.Reference

[<AllowNullLiteral>]
type NamedNodeMap(r : JSObject) =
    inherit JsObj(r)

    member x.Length = r.GetObjectProperty("length") |> unbox<int>

    member x.Set(value : Attr) =
        if isNull value.NamespaceURI then   
            printfn "setNamedItem"
            r.Invoke("setNamedItem", js value) |> ignore
        else
            printfn "setNamedItemNS"
            r.Invoke("setNamedItemNS", js value) |> ignore

    member x.Remove(name : string) =
        x.Call("removeNamedItem", name) |> ignore
        
    member x.Remove(ns : string, name : string) =
        x.Call("removeNamedItemNS", ns, name) |> ignore
        
    member x.Item
        with get(i : int) = 
            r.Invoke("item", i) |> convert<Attr>
            
    member x.Item
        with get(name : string) = 
            r.Invoke("getNamedItem", name) |> convert<Attr>
            
    member x.Item
        with get(ns : string, name : string) = 
            r.Invoke("getNamedItemNS", ns, name) |> convert<Attr>

    interface System.Collections.IEnumerable with
        member x.GetEnumerator() =
            (Seq.init x.Length (fun i -> x.[i])).GetEnumerator() :> _
            
    interface System.Collections.Generic.IEnumerable<Attr> with
        member x.GetEnumerator() =
            (Seq.init x.Length (fun i -> x.[i])).GetEnumerator()

    new (o : JsObj) =
        NamedNodeMap o.Reference
