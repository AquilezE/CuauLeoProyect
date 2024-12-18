using Cliente.ServiceReference;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cliente.GameUserControllers
{

    public partial class MonsterSelectionWindow : Window
    {

        public int SelectedMonsterIndex { get; private set; } = -1;

        public MonsterSelectionWindow(ObservableCollection<MonsterDTO> monsters)
        {
            InitializeComponent();
            var gameCardMonsters = new ObservableCollection<ObservableCollection<GameCard>>();

            foreach (MonsterDTO monster in monsters)
            {
                var gameCards = new ObservableCollection<GameCard>();
                foreach (CardDTO cardDto in monster.BodyParts)
                {
                    gameCards.Add(new GameCard(cardDto));
                }

                gameCardMonsters.Add(gameCards);
            }

            DataContext = new { Monster = gameCardMonsters };

            MonsterList.Loaded += MonsterList_Loaded;
        }

        private void MonsterList_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (object item in MonsterList.Items)
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
                    var monsters =
                        ((dynamic)DataContext).Monster as ObservableCollection<ObservableCollection<GameCard>>;
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (monsters[i].Contains(clickedGameCard))
                        {
                            SelectedMonsterIndex = i;
                            DialogResult = true;
                            Close();
                            return;
                        }
                    }
                }
            }
        }

        private TChildItem FindVisualChild<TChildItem>(DependencyObject obj) where TChildItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is TChildItem)
                {
                    return (TChildItem)child;
                }
                else
                {
                    var childOfChild = FindVisualChild<TChildItem>(child);
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