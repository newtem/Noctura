# Noctura API
  The Noctura Extension system is designed to 
  safely extend functionality via NLua based Lua scripts.

Lua scripts have specific lifecycle functions
  onLoad(), onRun(), onUnload()
and APIs provided by C#

## lifecycle functions

### onLoad()
Called once when the extension is loaded.
```lua
function onLoad()
    info("Extension loaded")
end
```

### onRun(code)
Called when a command is sent from C#.
```lua
function onRun(code)
    if code == "ping" then
        info("pong")
    end
end
```

### onUnload()
Called right before the extension is unloaded.
```lua
function onUnload()
    info("Extension unloaded")
end
```

## Log API

### info(message)
Logs an informational message.
`info("Hello Noctura")`

### warn(message)
Logs a warning message.
`warn("Something looks odd")`

### error(message)
Logs an error message.
`error("Something went wrong")`

## Core API
The core API is still being expanded and will be added to this .md later.

## Security restrictions
The following Lua APIs are blocked for security reasons:
- `os`
- `io`
- `package`
- `debug`
