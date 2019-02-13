# Debug Menu

It is easy to use, lightweight library initially forked from [wataru-ito/DebugMenu](https://github.com/wataru-ito/DebugMenu) but deeply modifyed.

The library renders as text, in game menu.  

```C#

// The fields to modify by menu
public enum TrafficLight { Red,Green, Blue }
public TrafficLight enumValue;
public bool toggleValue;
public int integerValue;
public float floatValue;
        
// Create new menu
new DebugMenu("Edit/Preferences");
new DebugMenuToggle("Edit/Preferences/Toggle", () => toggleValue, value => toggleValue = value, 1);
new DebugMenuInteger("Edit/Preferences/Integer", () => integerValue, value => integerValue = value, 2);
new DebugMenuFloat("Edit/Preferences/Float", () => floatValue, value => floatValue = value, 3);
new DebugMenuAction("Edit/Preferences/Action", (item,tag) => { Debug.Log("Action"); }, 4);
new DebugMenuEnum<TrafficLight>("Edit/Preferences/TraficLight", () => enumValue, value => enumValue = value, 5);
new DebugMenu("Edit/Preferences/Extra Preferences", 10);
```

![Picture1](Documentation/menu-picture1.png)
![Picture2](Documentation/menu-picture2.png)
![Picture3](Documentation/menu-picture3.png)

Other way is to add items directly to menu.

```C#
var menu, new DebugMenu("Edit/Preferences");
new DebugMenuToggle(menu, "Toggle", () => toggleValue, value => toggleValue = value, 1);
new DebugMenuInteger( menu, "Integer",() => integerValue, value => integerValue = value, 2);
new DebugMenuFloat(menu, "Float", () => floatValue, value => floatValue = value, 3);
new DebugMenuAction(menu, "Action", (item,tag) => { Debug.Log("Action"); }, 4);
new DebugMenuEnum<TrafficLight>(menu, "TraficLight", () => enumValue, value => enumValue = value, 5);
```

## Default Value

For integer, floats and enum types creating new DebugMenuItem will capture current value as defaut. When value is default it displayed as bright green color. Information about other color tags see below.

- **booleans**
  - yellow _color of label for enabed feture_
  - white _color of label for disabled feature_
- **integers**, **floats**, **enums**
  - bright green _color of value for default_ 
  - yellow _color of value and label for not default value_
- **actions**
  - gray _color for inactive action_
  - other _color for active action_ 

## Keyboard Shortcuts

- E show or hide menu without closing it
- ESC close current menu and display previous, or hide menu if there are no more
- W,S move previous and next menu item
- A,D edit menu item
- R reset value to default

## Events

Menu manager sends messages to menu items for rendering and modifying items .

```C#
public enum EvenTag
{
    Null,               //< Nothing 
    Render,             //< Render item, update label, value and colors
    Dec,                //< Decrease value or call action
    Inc,                //< Increase value or call action
    Prev,               //< Go to previous item 
    Next,               //< Go to next item
    Reset,              //< Reset value to default
    OpenMenu,           //< When menu open    
    CloseMenu           //< When menu closed
}
```
## Actions

The action code can update the item's fields, and differently response for events: Inc,Dec and Reset

```C#
new DebugMenuAction("Edit/Preferences/Action", (item,tag) => { 
        switch (tag)
        {
        case EventTag.Inc:
           item.value = "Inc";
           break;
        case EventTag.Dec:
           item.value = "Dec";
           break;
        ...
        }
}, 1);
```

## Open/Close Menu Event

Possible to add menu items when menu opens, and remove items when it closes.

```C#
new DebugMenu("Edit/Preferences/Extra Preferences", 30)
    .OnOpen(menu => 
    {
        new DebugMenuToggle("Toggle2", menu, () => toggleValue, value => toggleValue = value);
    })
    .OnClose(menu =>
    {
        menu.Clear();
    });
```

## Refresh menu and AutoRefresh Menu

If values in menu can be modified by game, to display it the menu should be rendered time to time.

```C#
new DebugMenu("Edit/Preferences").AutoRefresh(1f);
```

Set value 0 will never refresh menu. Alternative way is calling _RequestRefresh_ method.

```C#
menu.RequestRefresh()
```
## Increment & Precision

**For integers** and **floats** The vaue _increment_ is a step for _Inc_ and _Dec_ evens.  The value _format_ is argument for ToString(format) method.

**For floats** The value _precision_ is number of digits after period. For example _increment=5_ and _precision=2_ will make steps 0.05

## Syntax Sugar

```C#
// For DebugMenu class
DebugMenu OnOpen(Action<DebugMenu> onOpen)
DebugMenu OnClose(Action<DebugMenu> onClose)
DebugMenu AutoRefresh(float period)

// For MenuItem class
DebugMenuItem Order(int order)
DebugMenuItem AddToMenu(DebugMenu menu)
DebugMenuItem Value(string value)
DebugMenuItem LabelColor(string value)
DebugMenuItem ValueColor(string value)

// For enum value
DebugMenuEnum<T> Default(T value)

// For integer
DebugMenuInteger Default(int value)
DebugMenuInteger Increment(int value)
DebugMenuInteger Format(string value)

// For float
DebugMenuFloat Default(float value)
DebugMenuFloat Precision(int value)
DebugMenuFloat Increment(int value)
DebugMenuFloat Format(string value)
```




