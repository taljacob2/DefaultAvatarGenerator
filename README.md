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
DefaultAvatarGenerator.GenerateAvatar(color, initials);

// Geometric soft circles
DefaultAvatarGenerator.GenerateAvatarWithPattern(color, initials);

// Mosaic (pixel block style)
DefaultAvatarGenerator.GenerateAvatarWithMosaic(color, initials);

// Trading bars (finance-inspired)
DefaultAvatarGenerator.GenerateAvatarWithTradingBars(color, initials);
```
