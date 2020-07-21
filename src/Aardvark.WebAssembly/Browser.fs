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
        let o = r.Invoke("getBoundingClientRect") |> unbox<JsObj>
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

    // TODO: ShadowRoot
    //       OpenOrClosedShadowRoot
    //       Slot
    //       Animate
    //       GetAnimations
    //       AttachShadow
    //       CreateShadowRoot
    //       GetClientRects

    new (o : JsObj) =
        Element o.Reference

        
[<AllowNullLiteral>]
type HTMLCollection(r : JSObject) =
    inherit JsObj(r)

[<AllowNullLiteral>]
type Blob(r : JSObject) =
    inherit JsObj(r)


[<AllowNullLiteral>]
type CSSStyleDeclaration(r : JSObject) =
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
