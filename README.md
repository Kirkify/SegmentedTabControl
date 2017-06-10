[![Stories in Ready](https://badge.waffle.io/Kirkify/SegmentedTabControl.png?label=ready&title=Ready)](https://waffle.io/Kirkify/SegmentedTabControl?utm_source=badge)
# Segmented Tab Control Plugin for Xamarin Forms

#### Setup
* Available on NuGet: https://www.nuget.org/packages/SegmentedTabControl.FormsPlugin/ [![NuGet](https://img.shields.io/nuget/v/SegmentedTabControl.FormsPlugin.svg?label=NuGet)](https://www.nuget.org/packages/SegmentedTabControl.FormsPlugin/)
* Install ONLY in your PCL project.

**All Platforms Supported**

|Platform|Supported|Version|
| ------------------- | :-----------: | :-----------: |
|Xamarin.iOS Unified|Yes|Any
|Xamarin.Android|Yes|Any
|Windows|Yes|8+
|Windows Phone|Yes|8.1+
|Xamarin.iOS Classic|Yes|Any
|Xamarin.Mac Unified|Yes|Any
|Xamarin.TVOS|Yes|Any
|Xamarin.WatchOS|Yes|Any

#### Usage

This only needs to be installed in your PCL, no iOS Renderers, no Android Renderers, Nice and Simple ;)

#### XAML

```xml
xmlns:controls="clr-namespace:SegmentedTabControl.FormsPlugin;assembly=SegmentedTabControl"
```

```xml
<controls:SegmentedTabControl UnselectedSegmentBackgroundColor="Blue" TintColor="#007AFF" SelectedSegment="0">
  <controls:SegmentedControl.Children>
    <Label Text="Tab 1" />
    <Label Text="Tab 2" />
    <Label Text="Tab 3" />
    <Label Text="Tab 4" />
  </controls:SegmentedControl.Children>
</controls:SegmentedControl>
```

#### Event handler

```
private void SegmentedControl_ItemTapped(object sender, int key)
{
	
	switch (e)
	{
		case 0:
			Console.WriteLine($"Selected: {key})";
			break;
		case 1:
			Console.WriteLine($"Selected: {key})";
			break;
		case 2:
			Console.WriteLine($"Selected: {key})";
			break;
		// If set to -1 then NO segments will be selected
		default:
			Console.WriteLine($"No Segments Selected: {key}";
			break;
	}
}
```

**Bindable Properties**

#### Note the ```UnselectedSegmentBackgroundColor``` property always needs to be set, and should match the backgroundcolor of it's parent element.
```UnselectedSegmentBackgroundColor```: Fill color for the all the unselected segment options (Color, default Transparent)

```TintColor```: Fill color for the control (Color, default #007AFF)

```SelectedSegmentTextColor```: Selected segment text color (Color, default #FFFFFF)

```UnselectedSegmentTextColor```: Unselected segment text colors (Color, default #FF$

```SelectedSegment```: Selected segment index (int, default 0).

**Event Handlers**

```ItemTapped```: Called when a segment is selected.

**Commands**

```Command```: Called when a segment is selected.

#### Roadmap

* Vertical Segmented Tab Control support
* UWP support

#### Release Notes

1.0.0

First release

#### Contributors
* [kirkify](https://github.com/kirkify)

Thanks!

#### License
Licensed under MIT
