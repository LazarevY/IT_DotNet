#nullable enable
using Task04Lib;
using System;
using System.IO;
using GLib;
using Gtk;
using Pango;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task04GUI
{
    class MainWindow : Window
    {
        [UI] private Entry _diamondNameEdit;
        [UI] private SpinButton _weightEdit;
        [UI] private ComboBox _facetingQualityChoose;
        [UI] private ComboBox _colorChoose;
        [UI] private TextView _colorizedDiamondPreview;
        [UI] private TextView _diamondPreview;

        [UI] private ListStore _facetingQualityStore;

        private ColorizedDiamond _colorizedDiamond;
        private Diamond _diamond;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            Gtk.CssProvider provider = new CssProvider();
            string stylePath =
                System.IO.Path.Combine(
                    Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                    "style.css");
            provider.LoadFromPath(stylePath);

            Gtk.Window window = (Gtk.Window)builder.GetObject("start");
            Gtk.StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);


            _colorizedDiamond = new ColorizedDiamond("Diamond",
                0.01,
                1,
                DiamondColor.White);
            _diamond = new Diamond(
                _colorizedDiamond.Name,
                _colorizedDiamond.Weight,
                _colorizedDiamond.FacetingQuality);
            _diamondNameEdit.Text = "Diamond";

            DeleteEvent += Window_DeleteEvent;
            _diamondNameEdit.Changed += ChangeDiamondName;
            _weightEdit.ValueChanged += ChangeWeight;
            _facetingQualityChoose.Changed += ChangeFaceting;
            _colorChoose.Changed += ChangeColor;


            UpdateDiamondInfo();

            _colorChoose.Active = 0;
            _facetingQualityChoose.Active = 0;

        }

        private void UpdateDiamondInfo()
        {
            _diamondPreview.Buffer.Text = _diamond.ToString();
            _colorizedDiamondPreview.Buffer.Text = _colorizedDiamond.ToString();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void ChangeDiamondName(object? sender, EventArgs eventArgs)
        {
            _colorizedDiamond.Name = _diamondNameEdit.Text;
            _diamond.Name = _diamondNameEdit.Text;
            UpdateDiamondInfo();
        }        
        private void ChangeWeight(object? sender, EventArgs eventArgs)
        {
            _colorizedDiamond.Weight = _weightEdit.Value;
            _diamond.Weight = _weightEdit.Value;
            UpdateDiamondInfo();
        }        
        private void ChangeFaceting(object? sender, EventArgs eventArgs)
        {
            TreeIter active;
            _facetingQualityChoose.GetActiveIter(out active);
            uint value = (uint) _facetingQualityChoose.Model.GetValue(active, 0);
            _diamond.FacetingQuality = (int) value;
            _colorizedDiamond.FacetingQuality = (int) value;
            UpdateDiamondInfo();
        }        
        private void ChangeColor(object? sender, EventArgs eventArgs)
        {
            TreeIter active;
            _colorChoose.GetActiveIter(out active);
            String value = (String) _colorChoose.Model.GetValue(active, 1);
            Enum.TryParse(value, out DiamondColor c);
            _colorizedDiamond.Color = c;
            UpdateDiamondInfo();
        }
        
    }
}