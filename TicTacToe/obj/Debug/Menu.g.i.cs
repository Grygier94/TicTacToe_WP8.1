﻿

#pragma checksum "D:\Repositories\TicTacToe_WP8.1\TicTacToe\Menu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C6C5C1BEF5FD27E62A93017EA1196B25"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicTacToe
{
    partial class Menu : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Grid MenuGrid; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button btnSinglePlayer; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button btnMultiPlayer; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox tbPlayerNameX; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox tbPlayerNameO; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Menu.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            MenuGrid = (global::Windows.UI.Xaml.Controls.Grid)this.FindName("MenuGrid");
            btnSinglePlayer = (global::Windows.UI.Xaml.Controls.Button)this.FindName("btnSinglePlayer");
            btnMultiPlayer = (global::Windows.UI.Xaml.Controls.Button)this.FindName("btnMultiPlayer");
            tbPlayerNameX = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("tbPlayerNameX");
            tbPlayerNameO = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("tbPlayerNameO");
        }
    }
}



