﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Lógica de interacción para Card.xaml
    /// </summary>
    public partial class CardUserController : UserControl
    {


        public static readonly RoutedEvent CardClickedEvent = EventManager.RegisterRoutedEvent(
            "CardClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CardUserController));

        public event RoutedEventHandler CardClicked
        {
            add { AddHandler(CardClickedEvent, value); }
            remove { RemoveHandler(CardClickedEvent, value); }
        }


        public CardUserController()
        {
            InitializeComponent();
        }

        private void OnCardClicked()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CardClickedEvent, this);
            RaiseEvent(newEventArgs);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnCardClicked();
        }
    }
}
