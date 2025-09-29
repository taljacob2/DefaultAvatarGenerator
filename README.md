# DefaultAvatarGenerator

A lightweight utility to generate avatar images with background patterns and centered text (e.g. user initials).

Useful for default user profile images in apps like Jira, GitHub, or custom platforms.

## How Colors Are Picked?

Based on a given string, we pick a corresponding consistent color.

For example, you can use the "user initials" as a seed to get its corresponding color.

```csharp
string initials = "JD";

// Get a consistent color based on initials.
Color userColor = ColorPicker.GetRandomColor(initials);
```

## Avatar Variants

You can generate avatars using different styles:

```csharp
// Clean solid background
DefaultAvatarGenerator.GenerateAvatar(userColor, initials);

// Geometric soft circles
DefaultAvatarGenerator.GenerateAvatarWithPattern(userColor, initials);

// Mosaic (pixel block style)
DefaultAvatarGenerator.GenerateAvatarWithMosaic(userColor, initials);

// Trading bars (finance-inspired)
DefaultAvatarGenerator.GenerateAvatarWithTradingBars(userColor, initials);
```

## Examples

<img width="100" height="100" alt="avatar_pattern_7608a12a-2623-417e-8f80-c2d41e634ff6" src="https://github.com/user-attachments/assets/e86104d8-8857-4f63-806a-afec580b7256" />
<img width="100" height="100" alt="avatar_pattern_e56371f1-ac0a-439e-969b-89a1543cdd23" src="https://github.com/user-attachments/assets/55b77832-dbb6-4636-a46f-3b04ad72a781" />
<img width="100" height="100" alt="avatar_mosaic_d4cbf979-8837-488e-a046-168e83173662" src="https://github.com/user-attachments/assets/824e61c0-36ff-4406-b432-a4ac9bdac4b9" />
<img width="100" height="100" alt="avatar_tradingbars_72646afc-f06a-44fe-b94a-3efacee9a49d" src="https://github.com/user-attachments/assets/fb2845bc-d0a9-436e-b820-cc0ffd602e02" />
<img width="100" height="100" alt="avatar_fbacc80b-6ccd-446d-83cd-82b24e76f603" src="https://github.com/user-attachments/assets/851fbfb3-03cb-4342-a8da-a7000820d47b" />
<img width="100" height="100" alt="avatar_mosaic_9f270a8e-97eb-4a0c-b8a4-ea67ff0a9a75" src="https://github.com/user-attachments/assets/eee16b09-9419-48e1-b84f-227bbe6305a2" />

