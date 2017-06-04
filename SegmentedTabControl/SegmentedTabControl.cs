using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SegmentedTabControl.FormsPlugin
{
  public class SegmentedTabControl : Grid
  {
    public static readonly BindableProperty TintColorProperty =
        BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(SegmentedTabControl), Color.FromHex("#007AFF"), propertyChanged: HandleTintColorPropertyChanged);

    public Color TintColor
    {
      get { return (Color)GetValue(TintColorProperty); }
      set { SetValue(TintColorProperty, value); }
    }

    private static void HandleTintColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var control = (SegmentedTabControl)bindable;
      control.BackgroundColor = (Color)newValue;
    }

    public static readonly BindableProperty UnselectedSegmentBackgroundColorProperty =
        BindableProperty.Create(nameof(UnselectedSegmentBackgroundColor), typeof(Color), typeof(SegmentedTabControl), Color.Transparent, propertyChanged: HandleUnselectedSegmentBackgroundColorPropertyChanged);

    public Color UnselectedSegmentBackgroundColor
    {
      get { return (Color)GetValue(UnselectedSegmentBackgroundColorProperty); }
      set { SetValue(UnselectedSegmentBackgroundColorProperty, value); }
    }

    private static void HandleUnselectedSegmentBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      // Get a referemce to the SegmentedControl
      var control = (SegmentedTabControl)bindable;

      // Loop through all it's children
      for (var i = 0; i < control.Children.Count(); i++)
      {
        // As long as it's not the Selected Segment
        // (The Selected Segment uses the TintColorProperty as it's background color)
        if (i != control.SelectedSegment)
        {
          // Update all the unselected segments to the new background color
          control.Children[i].BackgroundColor = (Color)newValue;
        }
      }
    }

    public static readonly BindableProperty SelectedSegmentTextColorProperty =
        BindableProperty.Create(nameof(SelectedSegmentTextColor), typeof(Color), typeof(SegmentedTabControl), Color.FromHex("#FFFFFF"), propertyChanged: HandleSelectedSegmentTextColorPropertyChanged);

    public Color SelectedSegmentTextColor
    {
      get { return (Color)GetValue(SelectedSegmentTextColorProperty); }
      set { SetValue(SelectedSegmentTextColorProperty, value); }
    }

    private static void HandleSelectedSegmentTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      // Get a referemce to the SegmentedControl
      var control = (SegmentedTabControl)bindable;

      // Loop through all it's children
      for (var i = 0; i < control.Children.Count(); i++)
      {
        // Find the Selected Segment only
        if (i == control.SelectedSegment)
        {
          // Try casting it to a label
          var label = control.Children[i] as Label;

          // If it's a label
          if (label != null)
          {
            // Update it's text color
            label.TextColor = (Color)newValue;
          }
          // No need to continue any further
          break;
        }
      }
    }

    public static readonly BindableProperty UnselectedSegmentTextColorProperty =
        BindableProperty.Create(nameof(UnselectedSegmentTextColor), typeof(Color), typeof(SegmentedTabControl), Color.FromHex("#FFFFFF"), propertyChanged: HandleUnselectedSegmentTextColorPropertyChanged);

    public Color UnselectedSegmentTextColor
    {
      get { return (Color)GetValue(UnselectedSegmentTextColorProperty); }
      set { SetValue(UnselectedSegmentTextColorProperty, value); }
    }

    private static void HandleUnselectedSegmentTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      // Get a referemce to the SegmentedControl
      var control = (SegmentedTabControl)bindable;

      // Loop through all it's children
      for (var i = 0; i < control.Children.Count(); i++)
      {
        // Find the Unselected Segments only
        if (i != control.SelectedSegment)
        {
          // Try casting it to a label
          var label = control.Children[i] as Label;

          // If it's a label
          if (label != null)
          {
            // Update it's text color
            label.TextColor = (Color)newValue;
          }
        }
      }
    }

    public static readonly BindableProperty SelectedSegmentProperty =
        BindableProperty.Create(nameof(SelectedSegment), typeof(int), typeof(SegmentedTabControl), 0, BindingMode.TwoWay, propertyChanged: HandleSelectedSegmentPropertyChanged);

    public int SelectedSegment
    {
      get { return (int)GetValue(SelectedSegmentProperty); }
      set { SetValue(SelectedSegmentProperty, value); }
    }

    private static void HandleSelectedSegmentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var control = (SegmentedTabControl)bindable;
      var selection = (int)newValue;

      control.UpdateSelectedItem(selection);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SegmentedTabControl), null, propertyChanged: (bo, o, n) => ((SegmentedTabControl)bo).OnCommandChanged());

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SegmentedTabControl), null,
      propertyChanged: (bindable, oldvalue, newvalue) => ((SegmentedTabControl)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

    public ICommand Command
    {
      get { return (ICommand)GetValue(CommandProperty); }
      set { SetValue(CommandProperty, value); }
    }

    public object CommandParameter
    {
      get { return GetValue(CommandParameterProperty); }
      set { SetValue(CommandParameterProperty, value); }
    }

    protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
    {
      if (propertyName == CommandProperty.PropertyName)
      {
        ICommand cmd = Command;
        if (cmd != null)
          cmd.CanExecuteChanged -= CommandCanExecuteChanged;
      }
      base.OnPropertyChanging(propertyName);
    }

    void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
    {
      ICommand cmd = Command;
      if (cmd != null)
        IsVisible = cmd.CanExecute(CommandParameter);
    }

    void OnCommandChanged()
    {
      if (Command != null)
      {
        Command.CanExecuteChanged += CommandCanExecuteChanged;
        CommandCanExecuteChanged(this, EventArgs.Empty);
      }
      else
        IsVisible = true;
    }

    public event EventHandler<int> ItemTapped = (e, a) => { };

    public SegmentedTabControl()
    {
      Padding = new Thickness(1);
      ColumnSpacing = 1;
      BackgroundColor = TintColor;
    }

    private void UpdateSelectedItem(int key)
    {
      for (var i = 0; i < Children.Count(); i++)
      {
        var label = Children[i] as Label;

        if (label != null)
        {
          label.BackgroundColor = UnselectedSegmentBackgroundColor;
          label.TextColor = UnselectedSegmentTextColor;
        }
      }

      if (key < Children.Count() && key >= 0)
      {
        Children[key].BackgroundColor = TintColor;

        var selectedItem = Children[key] as Label;
        if (selectedItem != null)
        {
          selectedItem.TextColor = SelectedSegmentTextColor;
        }
        ItemTapped(this, key);
        CommandParameter = key;
        Command?.Execute(CommandParameter);
      }
      else
      {
        SelectedSegment = -1;
      }

    }

    private void ItemTappedCommand(int key)
    {
      SelectedSegment = key;
    }

    protected override void OnChildAdded(Element child)
    {
      base.OnChildAdded(child);

      var currentCount = Children.Count() - 1;
      var label = child as Label;

      if (label != null && currentCount >= 0)
      {
        label.VerticalTextAlignment = TextAlignment.Center;
        label.HorizontalTextAlignment = TextAlignment.Center;
        label.BackgroundColor = UnselectedSegmentBackgroundColor;

        label.GestureRecognizers.Add(new TapGestureRecognizer()
        {
          Command = new Command<int>(ItemTappedCommand),
          CommandParameter = currentCount
        });

        if (currentCount == SelectedSegment)
        {
          label.BackgroundColor = TintColor;
          label.TextColor = SelectedSegmentTextColor;
        }

        SetColumn(label, currentCount);
      }


    }
  }

}
