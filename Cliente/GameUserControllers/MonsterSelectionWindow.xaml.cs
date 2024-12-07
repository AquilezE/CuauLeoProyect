using Cliente.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cliente.GameUserControllers
{
    /// <summary>
    /// Interaction logic for MonsterSelectionWindow.xaml
    /// </summary>
    public partial class MonsterSelectionWindow : Window
    {
        public int SelectedMonsterIndex { get; private set; } = -1;

        public MonsterSelectionWindow(ObservableCollection<MonsterDTO> monsters)
        {
            InitializeComponent();
            var gameCardMonsters = new ObservableCollection<ObservableCollection<GameCard>>();

            foreach (var monster in monsters)
            {
                var gameCards = new ObservableCollection<GameCard>();
                foreach (var cardDto in monster.BodyParts)
                {
                    gameCards.Add(new GameCard(cardDto));
                }
                gameCardMonsters.Add(gameCards);
            }

            this.DataContext = new { Monster = gameCardMonsters };

            MonsterList.Loaded += MonsterList_Loaded;
        }

        private void MonsterList_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in MonsterList.Items)
            {
                var container = MonsterList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container != null)
                {
                    var cardControl = FindVisualChild<CardUserController>(container);
                    if (cardControl != null)
                    {
                        cardControl.CardClicked += CardControl_CardClicked;
                    }
                }
            }
        }

        private void CardControl_CardClicked(object sender, RoutedEventArgs e)
        {
            var clickedCard = sender as CardUserController;
            if (clickedCard != null)
            {
                var clickedGameCard = clickedCard.DataContext as GameCard;
                if (clickedGameCard != null)
                {
                    var monsters = ((dynamic)this.DataContext).Monster as ObservableCollection<ObservableCollection<GameCard>>;
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (monsters[i].Contains(clickedGameCard))
                        {
                            SelectedMonsterIndex = i;
                            this.DialogResult = true;
                            this.Close();
                            return; 
                        }
                    }
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
