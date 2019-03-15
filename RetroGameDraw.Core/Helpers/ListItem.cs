namespace RetroGameDraw.Core.Helpers
{
    #region --- Usings ----

    using System.Windows.Input;
    using System.ComponentModel;
    using MvvmCross.Localization;

    #endregion

    /// <summary>
    /// Encapsulates an item inside a list and provides
    /// it with additional commands 
    /// </summary>
    public class ListItem<T> : INotifyPropertyChanged
    {
        #region --- Variables ---- 

        private bool isSelected;

        #endregion


        #region --- Properties ---- 

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Item
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        /// Edit the item
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// Accepts / completed the item
        /// </summary>
        public ICommand OkCommand { get; set; }

        /// <summary>
        /// Indicates if an item is being currently selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                this.isSelected = value;
                OnPropertyChanged("IsSelected");
                OnPropertyChanged("IsSelectedInverted");
            }
        }

        /// <summary>
        /// Opposite to IsSelected
        /// </summary>
        public bool IsSelectedInverted => !IsSelected;

        /// <summary>
        /// Defines the source file for texts
        /// </summary>
        public IMvxLanguageBinder TextSource { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SeedlingSupply.Dispatch.Core.Helpers.ListItem`1"/>
        /// last item.
        /// </summary>
        /// <value><c>true</c> if last item; otherwise, <c>false</c>.</value>
        public bool IsLastItem { get; set; }

        #endregion


        #region --- Constructor ----

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item"></param>
        public ListItem(T item)
        {
            this.Item = item;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Wrapped Item</param>
        /// <param name="textSource">Text Source for text localization purpose</param>
        public ListItem(T item, IMvxLanguageBinder textSource)
        {
            this.Item = item;
            this.TextSource = textSource;
        }
        #endregion


        #region --- OnPropertyChanged ----


        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
