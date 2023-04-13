# Exercise Notes
## Coding Conventions
- Be mindful of correct spelling and grammar.
- Methods are written in **PascalCase()**, variables usually in **camelCase**.
- Use Properties instead of Getters/setters that you would usually find in Java (https://www.w3schools.com/cs/cs_properties.php).

## Importing Assets
- For Pixel Art use Filter Mode: Point, Compression: None and PPU 16/32 (1 Unit ~ 1 meter)
- Consider creating presets for your importers and Components
- Presets can be managed under Edit -> Project Settings -> Preset Manager

## Exercise hints
- Set the sorting order **before** you create your world
- You can deactivate a parent GameObjects together with all of their children with **gameObject.SetActive(false)**
- To access multiple SpriteRenderers, either use an array: `[SerializeField] SpriteRenderer[] renderers`, or use `GetComponentsInChildren()` (https://docs.unity3d.com/ScriptReference/Component.GetComponentsInChildren.html)
