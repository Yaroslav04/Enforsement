﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;
using Enforsement.Core.ViewModel;
using Enforsement.Core.Model;
using Enforsement.Core.Servise;

namespace Enforsement.Core.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {
        public StartPageViewModel()
        {
            Debug.WriteLine(FileSystem.Current.AppDataDirectory);
            ItemTappedSingle = new Command<EnforsementClassSoket>(OnSingleItemTapped);
            ItemTappedDouble = new Command<EnforsementClassSoket>(OnDoubleItemTapped);
            AddCommand = new Command(Add);
            SearchCommand = new Command(Search);
            ClearCommand = new Command(Clear);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            ConsoleCommand = new Command(ConsoleInput);
            CompleteCommand = new Command(виконано);
            CloneCommand = new Command(Clone);

            Items = new ObservableCollection<EnforsementClassSoket>();
            TypeSearchPanel = new ObservableCollection<string>
            {
                "Тимчасовий доступ", "Арешт майна", "Обшук"
            };
            QualificationsSearchPanel = new ObservableCollection<string>();
            for ( int i =110; i < 448; i++)
            {
                QualificationsSearchPanel.Add(i.ToString());
            }
            ExecuteSearchPanel = new ObservableCollection<string>
            {
                "На виконанні", "Виконані", "Прострочені"
            };
            Courts = new ObservableCollection<string>
            {
                "Заводський районний суд", "Дніпровський районний суд", "Баглійський районний суд"
            };
            SortStates = new ObservableCollection<string>()
            {
                "За зростанням по даті винесення", "За спаданням по даті винесення", "За зростанням по контрольній даті", "За спаданням по контрольній даті"
            };

            RunAsync();
        }

        private async void RunAsync()
        {
            SortState = "За зростанням по контрольній даті";
            SelectedExecuteSearchPanel = "Прострочені";
            await LoadItems();
        }

        public void OnAppearing()
        {

        }

        #region Properties
        public ObservableCollection<EnforsementClassSoket> Items { get; }
        public ObservableCollection<string> Courts { get; }

        public EnforsementClassSoket selectedItem = null;

        private string searchTextSearchPanel = null;
        public string SearchTextSearchPanel
        {
            get => searchTextSearchPanel;
            set
            {
                SetProperty(ref searchTextSearchPanel, value);
            }
        }
        public ObservableCollection<string> TypeSearchPanel { get; }

        public ObservableCollection<string> SortStates { get; }

        private string sortState = "За зростанням по контрольній даті";
        public string SortState
        {
            get => sortState;
            set
            {
                SetProperty(ref sortState, value);
                //if (value != null)
                //{
                //    LoadItems();
                //}
            }
        }

        private string selectedTypeSearchPanel = null;
        public string SelectedTypeSearchPanel
        {
            get => selectedTypeSearchPanel;
            set
            {
                SetProperty(ref selectedTypeSearchPanel, value);
                //if (value != null)
                //{
                //    LoadItems();
                //}
                
            }
        }
        public ObservableCollection<string> QualificationsSearchPanel { get; }

        private string selectedQualificationSearchPanel = null;
        public string SelectedQualificationSearchPanel
        {
            get => selectedQualificationSearchPanel;
            set
            {
                SetProperty(ref selectedQualificationSearchPanel, value);
                //if (value != null)
                //{
                //    LoadItems();
                //}
            }
        }

        public ObservableCollection<string> ExecuteSearchPanel { get; }

        private string selectedExecuteSearchPanel = null;
        public string SelectedExecuteSearchPanel
        {
            get => selectedExecuteSearchPanel;
            set
            {
                SetProperty(ref selectedExecuteSearchPanel, value);
                //if (value != null)
                //{
                //    LoadItems();
                //}
            }
        }

        private string statusDescription = null;
        public string StatusDescription
        {
            get => statusDescription;
            set
            {
                SetProperty(ref statusDescription, value);
            }
        }

        private string idDescription = null ;
        public string IdDescription
        {
            get => idDescription;
            set
            {
                SetProperty(ref idDescription, value);
            }
        }

        private string typeSelectedDescription = null ;
        public string TypeSelectedDescription
        {
            get => typeSelectedDescription;
            set
            {
                SetProperty(ref typeSelectedDescription, value);
            }
        }

        private string criminalNumberDescription = null ;
        public string CriminalNumberDescription
        {
            get => criminalNumberDescription;
            set
            {
                SetProperty(ref criminalNumberDescription, value);
            }
        }

        private string initDateDescription = null ;
        public string InitDateDescription
        {
            get => initDateDescription;
            set
            {
                SetProperty(ref initDateDescription, value);
            }
        }

        private string controlDateDescription = null ;
        public string ControlDateDescription
        {
            get => controlDateDescription;
            set
            {
                SetProperty(ref controlDateDescription, value);
            }
        }

        private string investigatorDescription = null ;
        public string InvestigatorDescription
        {
            get => investigatorDescription;
            set
            {
                SetProperty(ref investigatorDescription, value);
            }
        }

        private string selectedQualificationDescription = null ;
        public string SelectedQualificationDescription
        {
            get => selectedQualificationDescription;
            set
            {
                SetProperty(ref selectedQualificationDescription, value);
            }
        }

        private string courtDescription = null ;
        public string CourtDescription
        {
            get => courtDescription;
            set
            {
                SetProperty(ref courtDescription, value);
            }
        }

        private string descriptionDescription = null ;
        public string DescriptionDescription
        {
            get => descriptionDescription;
            set
            {
                SetProperty(ref descriptionDescription, value);
            }
        }

        #endregion

        #region Command

        public Command<EnforsementClassSoket> ItemTappedSingle { get; }
        public Command<EnforsementClassSoket> ItemTappedDouble { get; }
        public Command AddCommand { get; }
        public Command SearchCommand { get; }
        public Command ClearCommand { get; }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }
        public Command ConsoleCommand { get; }
        public Command CompleteCommand { get; }
        public Command CloneCommand { get; }

        #endregion


        #region Main
        private void OnDoubleItemTapped(EnforsementClass _item)
        {

        }

        private void OnSingleItemTapped(EnforsementClassSoket _item)
        {
            if (_item != null)
            {
                selectedItem = _item;
                IdDescription = _item.Id.ToString();
                CriminalNumberDescription = _item.CriminalNumber;
                TypeSelectedDescription = _item.Type;
                SelectedQualificationDescription = _item.Qualification;
                InitDateDescription = _item.InitDate.ToShortDateString();
                ControlDateDescription = _item.ControlDate.ToShortDateString();
                InvestigatorDescription = _item.Investigator;
                CourtDescription = _item.Court;
                DescriptionDescription = _item.Description;
                StatusDescription = _item.Status;  
            }
        }

        private async void Clear()
        {
            selectedItem = null;
            DescriptionDescription = null;
            CourtDescription = null;
            SelectedQualificationDescription = null;
            InvestigatorDescription = null;
            ControlDateDescription = null;
            InitDateDescription = null;
            CriminalNumberDescription = null;
            TypeSelectedDescription = null;
            IdDescription = null;
            SelectedExecuteSearchPanel = null;
            SelectedQualificationSearchPanel = null;
            SelectedTypeSearchPanel = null;
            SearchTextSearchPanel = null;
            StatusDescription = null;
            await LoadItems();
        }

        private async void ConsoleInput()
        {
            SearchTextSearchPanel =  FileSystem.Current.AppDataDirectory;
        }

        private async void Delete()
        {
            if (selectedItem != null)
            {
                await App.DataBase.DeleteAsync(SoketToEnforsementClass.Convert(selectedItem));
            }
            Clear();
        }

        private void Edit()
        {
            if (selectedItem != null)
            {
                EnforsementClass enforsementClass = new EnforsementClass();
                enforsementClass.Id = Convert.ToInt32(IdDescription);
                enforsementClass.CriminalNumber = CriminalNumberDescription;
                enforsementClass.Type = TypeSelectedDescription;
                enforsementClass.Qualification = SelectedQualificationDescription;
                enforsementClass.InitDate = Convert.ToDateTime(InitDateDescription);
                enforsementClass.ControlDate = Convert.ToDateTime(ControlDateDescription);
                enforsementClass.Court = CourtDescription;
                enforsementClass.Investigator = InvestigatorDescription;
                enforsementClass.Description = DescriptionDescription;
                enforsementClass.Status = StatusDescription;
                App.DataBase.UpdateAsync(enforsementClass);
                Clear();
            }        
        }

        private void Search()
        {
            LoadItems();
        }

        private void виконано()
        {
            if (selectedItem != null)
            {
                EnforsementClass enforsementClass = new EnforsementClass();
                enforsementClass.Id = Convert.ToInt32(IdDescription);
                enforsementClass.CriminalNumber = CriminalNumberDescription;
                enforsementClass.Type = TypeSelectedDescription;
                enforsementClass.Qualification = SelectedQualificationDescription;
                enforsementClass.InitDate = Convert.ToDateTime(InitDateDescription);
                enforsementClass.ControlDate = Convert.ToDateTime(ControlDateDescription);
                enforsementClass.Court = CourtDescription;
                enforsementClass.Investigator = InvestigatorDescription;
                enforsementClass.Description = DescriptionDescription;
                enforsementClass.Status = "виконано";
                App.DataBase.UpdateAsync(enforsementClass);
                Clear();
            }
        }

        private async void Clone()
        {
            if (selectedItem != null)
            {
                EnforsementClass enforsementClass = new EnforsementClass();               
                enforsementClass.CriminalNumber = CriminalNumberDescription;
                enforsementClass.Type = TypeSelectedDescription;
                enforsementClass.Qualification = SelectedQualificationDescription;
                enforsementClass.InitDate = Convert.ToDateTime(InitDateDescription);
                enforsementClass.ControlDate = Convert.ToDateTime(ControlDateDescription);
                enforsementClass.Court = CourtDescription;
                enforsementClass.Investigator = InvestigatorDescription;              
                enforsementClass.Status = "на виконанні";
                string _description = await Shell.Current.DisplayPromptAsync($"Клонировать елемент", $"Введите описание:", maxLength: 300);
                enforsementClass.Description = _description;
                await App.DataBase.SaveAsync(enforsementClass);
                Clear();
            }
        }

        private async void Add()
        {
            EnforsementClass enforsementClass = new EnforsementClass();
            try
            {
                string _criminalNumber = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите номер уголовного дела:", maxLength: 17);
                if (!TextServise.IsNumberValid(_criminalNumber))
                {
                    await Shell.Current.DisplayAlert("Добавить новый елемент", $"Ошибка не верно введен номер дела", "OK");
                    return;
                }
                string _type = await Shell.Current.DisplayActionSheet("Выберите тип документа", "Cancel", "Destruction", "Тимчасовий доступ", "Арешт майна", "Обшук");
                string _initDate = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите дату документа, пример: 01.01.2022:", maxLength: 10, initialValue: DateTime.Now.ToShortDateString());
                if (!TextServise.IsDateValid(_initDate))
                {
                    await Shell.Current.DisplayAlert("Добавить новый елемент", $"Ошибка не верно введена дата", "OK");
                    return;
                }
                string _controlDate;
                if (_type != "Арешт майна")
                {
                    _controlDate = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите контрольную дату документа, пример: 01.01.2022:", maxLength: 10, initialValue: (Convert.ToDateTime(_initDate).AddDays(30)).ToShortDateString());
                    if (!TextServise.IsDateValid(_controlDate))
                    {
                        await Shell.Current.DisplayAlert("Добавить новый елемент", $"Ошибка не верно введена дата", "OK");
                        return;
                    }
                }
                else
                {
                    _controlDate = DateTime.MaxValue.ToShortDateString();
                }
                string _investigator = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите следователя", maxLength: 20);
                string _qualification = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите квалификацю - три цифры пример: 185", maxLength: 3);
                List<string> _courts = new List<string>(Courts);
                string _court = await Shell.Current.DisplayActionSheet("Выберите суд", "Cancel", "Destruction", _courts.ToArray());
                string _description = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введите описание", maxLength: 300);

                enforsementClass.CriminalNumber = _criminalNumber;
                enforsementClass.Type = _type;
                enforsementClass.InitDate = Convert.ToDateTime(_initDate);
                enforsementClass.ControlDate = Convert.ToDateTime(_controlDate);
                enforsementClass.Investigator = _investigator;
                enforsementClass.Qualification = _qualification;
                enforsementClass.Court = _court;
                enforsementClass.Description = _description;
                enforsementClass.Status = "на виконанні";
                await App.DataBase.SaveAsync(enforsementClass);
                await LoadItems();
                await Shell.Current.DisplayAlert("Добавить новый елемент", $"Зарегистрировано", "OK");
            }
            catch
            {
                await Shell.Current.DisplayAlert("Добавить новый елемент", $"Ошибка", "OK");
            }

        }

        async Task LoadItems()
        {
            Items.Clear();

            var _result = new List<EnforsementClassSoket>();

            foreach (var item in await App.DataBase.GetAllAsync())
            {
                EnforsementClassSoket enforsementClassSoket = new EnforsementClassSoket(item);
                enforsementClassSoket.InitDateSoket = item.InitDate.ToShortDateString();
                if (item.Type != "Арешт майна")
                {
                    enforsementClassSoket.ControlDateSoket = item.ControlDate.ToShortDateString();
                    enforsementClassSoket.Days = (item.ControlDate - DateTime.Now).Days;
                }
                else
                {
                    enforsementClassSoket.ControlDateSoket = "-";
                }
               

                string _typeIcon = "";
                switch (enforsementClassSoket.Type)
                {
                    case "Тимчасовий доступ":
                        _typeIcon = "📃";
                        break;
                    case "Арешт майна":
                        _typeIcon = "🔒";
                        break;
                    case "Обшук":
                        _typeIcon = "🔎";
                        break;
                }

                enforsementClassSoket.TypeIcon = _typeIcon;
                _result.Add(enforsementClassSoket);
            }

            if (SortState == "За зростанням по даті винесення")
            {
                _result = _result.OrderBy(x => x.InitDate).ToList();
            }
            else if(SortState == "За спаданням по даті винесення")
            {
                _result = _result.OrderByDescending(x => x.InitDate).ToList();
            }
            else if (SortState == "За зростанням по контрольній даті")
            {
                _result = _result.OrderBy(x => x.ControlDate).ToList();
            }
            else if (SortState == "За спаданням по контрольній даті")
            {
                _result = _result.OrderByDescending(x => x.ControlDate).ToList();
            }
            else
            {
                _result = _result.OrderByDescending(x => x.ControlDate).ToList();
            }


            if (SelectedTypeSearchPanel == "Тимчасовий доступ")
            {
                _result = _result.Where(x => x.Type == "Тимчасовий доступ").ToList();
            }
            else if (SelectedTypeSearchPanel == "Арешт майна")
            {
                _result = _result.Where(x => x.Type == "Арешт майна").ToList();
            }
            else if (SelectedTypeSearchPanel == "Обшук")
            {
                _result = _result.Where(x => x.Type == "Обшук").ToList();
            }
            else if (SelectedQualificationSearchPanel != null)
            {
                _result = _result.Where(x => x.Qualification == SelectedQualificationSearchPanel).ToList();
            }

            if (SelectedExecuteSearchPanel == "На виконанні")
            {
                _result = _result.Where(x => x.Status == "на виконанні").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Виконані")
            {
                _result = _result.Where(x => x.Status == "виконано").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Прострочені")
            {
                _result = _result.Where(x => x.ControlDate < DateTime.Now).ToList();
                _result = _result.Where(x => x.Status == "на виконанні").ToList();
                _result = _result.Where(x => x.Type != "Арешт майна").ToList();
            }

            if (SearchTextSearchPanel != null)
            {
                _result = _result.Where(x => x.Description.Contains(SearchTextSearchPanel, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        

            foreach (var item in _result)
            {
                Items.Add(item);
            }
            return;
        }

        #endregion

    }
}
