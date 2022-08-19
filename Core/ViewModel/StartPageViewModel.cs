using System;
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
            ItemTappedSingle = new Command<EnforsementClassSoket>(OnSingleItemTapped);
            AddCommand = new Command(Add);
            SearchCommand = new Command(Search);
            ClearCommand = new Command(Clear);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            ConsoleCommand = new Command(OpenFolder);
            CompleteCommand = new Command(Complete);
            CloneCommand = new Command(Clone);
            ExportCommand = new Command(Export);
            CanselCommand = new Command(Cansel);
            LostCommand = new Command(Lost);

            Items = new ObservableCollection<EnforsementClassSoket>();
            TypeSearchPanel = new ObservableCollection<string>
            {
                "Арешт майна", "Тимчасовий доступ", "Обшук", "Невідкладний обшук", "Огляд", "Затримання з метою приводу"
            };
            QualificationsSearchPanel = new ObservableCollection<string>();
            for (int i = 110; i < 448; i++)
            {
                QualificationsSearchPanel.Add(i.ToString());
            }
            ExecuteSearchPanel = new ObservableCollection<string>
            {
                "На виконанні", "Прострочені", "Відмовлені судом", "Виконані", "Пропущені"
            };
            Courts = new ObservableCollection<string>
            {
                "Заводський районний суд", "Дніпровський районний суд", "Баглійський районний суд"
            };
            SortStates = new ObservableCollection<string>()
            {
                "За зростанням по даті винесення", "За спаданням по даті винесення", "За зростанням по контрольній даті", "За спаданням по контрольній даті", "За порядковим номером"
            };

            RunAsync();
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

        private string idDescription = null;
        public string IdDescription
        {
            get => idDescription;
            set
            {
                SetProperty(ref idDescription, value);
            }
        }

        private string typeSelectedDescription = null;
        public string TypeSelectedDescription
        {
            get => typeSelectedDescription;
            set
            {
                SetProperty(ref typeSelectedDescription, value);
            }
        }

        private string criminalNumberDescription = null;
        public string CriminalNumberDescription
        {
            get => criminalNumberDescription;
            set
            {
                SetProperty(ref criminalNumberDescription, value);
            }
        }

        private string initDateDescription = null;
        public string InitDateDescription
        {
            get => initDateDescription;
            set
            {
                SetProperty(ref initDateDescription, value);
            }
        }

        private string controlDateDescription = null;
        public string ControlDateDescription
        {
            get => controlDateDescription;
            set
            {
                SetProperty(ref controlDateDescription, value);
            }
        }

        private string investigatorDescription = null;
        public string InvestigatorDescription
        {
            get => investigatorDescription;
            set
            {
                SetProperty(ref investigatorDescription, value);
            }
        }

        private string selectedQualificationDescription = null;
        public string SelectedQualificationDescription
        {
            get => selectedQualificationDescription;
            set
            {
                SetProperty(ref selectedQualificationDescription, value);
            }
        }

        private string courtDescription = null;
        public string CourtDescription
        {
            get => courtDescription;
            set
            {
                SetProperty(ref courtDescription, value);
            }
        }

        private string descriptionDescription = null;
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
        public Command AddCommand { get; }
        public Command SearchCommand { get; }
        public Command ClearCommand { get; }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }
        public Command ConsoleCommand { get; }
        public Command CompleteCommand { get; }
        public Command CloneCommand { get; }
        public Command ExportCommand { get; }
        public Command CanselCommand { get; }
        public Command LostCommand { get; }

        #endregion

        #region Main

        private async void RunAsync()
        {
            await StartControl();
            SortState = "За зростанням по контрольній даті";
            SelectedExecuteSearchPanel = "Прострочені";
            await LoadItems();
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
            StatusDescription = null;
            SelectedExecuteSearchPanel = null;
            SelectedQualificationSearchPanel = null;
            SelectedTypeSearchPanel = null;
            SearchTextSearchPanel = null;
            await LoadItems();
            return;
        }

        private void OpenFolder()
        {
            Process.Start("explorer.exe", FileSystem.Current.AppDataDirectory);
        }

        private void Search()
        {
            LoadItems();
        }

        private async void Delete()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Видалити?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
                {
                    await App.DataBase.DeleteAsync(SoketToEnforsementClass.Convert(selectedItem));

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
                    StatusDescription = null;
                    await LoadItems();
                }
            }
        }

        private async void Edit()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Редагувати ?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
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
                    var control = ItemControl(enforsementClass);
                    if (control != null)
                    {
                        enforsementClass = control;
                    }
                    await App.DataBase.UpdateAsync(enforsementClass);
                    await LoadItems();
                }
            }
            else
            {
                await Shell.Current.DisplayActionSheet($"Виберіть елемент зі списку", destruction: "OK", cancel: "Відміна");
            }
        }

        private async void Complete()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Виконано ?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
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
                    await App.DataBase.UpdateAsync(enforsementClass);
                    await LoadItems();
                }
            }
            else
            {
                await Shell.Current.DisplayActionSheet($"Виберіть елемент зі списку", destruction: "OK", cancel: "Відміна");
            }
        }

        private async void Clone()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Клонувати ?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
                {
                    int count = 0;
                    var n = await App.DataBase.GetAllAsync();
                    if (n.Count > 0)
                    {
                        count = n.Last().Id + 1;
                    }
                    EnforsementClass enforsementClass = new EnforsementClass();
                    enforsementClass.CriminalNumber = CriminalNumberDescription;
                    enforsementClass.Type = TypeSelectedDescription;
                    enforsementClass.Qualification = SelectedQualificationDescription;
                    enforsementClass.InitDate = Convert.ToDateTime(InitDateDescription);
                    enforsementClass.ControlDate = Convert.ToDateTime(ControlDateDescription);
                    enforsementClass.Court = CourtDescription;
                    enforsementClass.Investigator = InvestigatorDescription;
                    enforsementClass.Status = "на виконанні";
                    string _description = await Shell.Current.DisplayPromptAsync($"Клонувати елемент", $"Введіть опис:", maxLength: 300);
                    if (!String.IsNullOrWhiteSpace(_description))
                    {
                        enforsementClass.Description = _description;
                        await App.DataBase.SaveAsync(enforsementClass);
                        SortState = "За порядковим номером";
                        Clear();
                        await Shell.Current.DisplayAlert("Додати новий елемент", $"Зарєстровано, порядковий номер {count}", "OK");
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayActionSheet($"Виберіть елемент зі списку", destruction: "OK", cancel: "Відміна");
            }
        }

        private async void Export()
        {
            if (File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, "export.csv")))
            {
                File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "export.csv"));
            }

            using (StreamWriter sr = new StreamWriter(Path.Combine(FileSystem.Current.AppDataDirectory, "export.csv"), true))
            {
                string text = $"№\tТип\tНомер кримінального провадження\tДата ініціювання документу\tКонтрольна дата документу\tСлідчий\tКваліфікація\tСуд\tОпис\tСтатус";
                sr.WriteLine(text);
            }
            foreach (var item in await App.DataBase.GetAllAsync())
            {
                using (StreamWriter sr = new StreamWriter(Path.Combine(FileSystem.Current.AppDataDirectory, "export.csv"), true))
                {
                    string text = $"{item.Id}\t{item.Type}\t№{item.CriminalNumber}\t{item.InitDate.ToShortDateString()}\t{item.ControlDate.ToShortDateString()}\t{item.Investigator}\t{item.Qualification}\t{item.Court}\t{item.Description}\t{item.Status}";
                    sr.WriteLine(text);
                }
            }
            await Shell.Current.DisplayAlert("Експорт", $"Експорт виконано, перейдіть до системної папки📂", "OK");
        }

        private async void Cansel()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Відмовлено судом ?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
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
                    enforsementClass.Status = "відмовлено";
                    await App.DataBase.UpdateAsync(enforsementClass);
                    await LoadItems();
                }
            }
            else
            {
                await Shell.Current.DisplayActionSheet($"Виберіть елемент зі списку", destruction: "OK", cancel: "Відміна");
            }
        }

        private async void Lost()
        {
            if (selectedItem != null)
            {
                var result = await Shell.Current.DisplayActionSheet($"Пропущено строк виконання?", destruction: "OK", cancel: "Відміна");
                if (result == "OK")
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
                    enforsementClass.Status = "пропущено";
                    await App.DataBase.UpdateAsync(enforsementClass);
                    await LoadItems();
                }
            }
            else
            {
                await Shell.Current.DisplayActionSheet($"Виберіть елемент зі списку", destruction: "OK", cancel: "Відміна");
            }
        }

        private async void Add()
        {
            EnforsementClass enforsementClass = new EnforsementClass();
            try
            {
                int count = 1;
                var n = await App.DataBase.GetAllAsync();
                if (n.Count > 0)
                {
                    count = n.Last().Id + 1;
                }

                string _criminalNumber = await Shell.Current.DisplayPromptAsync($"Добавить новый елемент", $"Введіть номер кримінального провадження", maxLength: 17);
                if (!TextServise.IsNumberValid(_criminalNumber))
                {
                    await Shell.Current.DisplayAlert("Додати новий елемент", $"Не вірно вказаний номер провадження", "OK");
                    return;
                }

                List<string> _types = new List<string>(TypeSearchPanel);
                string _type = await Shell.Current.DisplayActionSheet("Виберіть тип документу", "Cancel", null, _types.ToArray());

                string _initDate = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть дату документу", maxLength: 10, initialValue: DateTime.Now.ToShortDateString());
                if (!TextServise.IsDateValid(_initDate))
                {
                    await Shell.Current.DisplayAlert("Додати новий елемент", $"Не вірно зазанчена дата", "OK");
                    return;
                }

                string _controlDate;
                if (_type == "Арешт майна")
                {
                    _controlDate = _initDate;
                }
                else if (_type == "Невідкладний обшук")
                {
                    _controlDate = _initDate;
                }
                else
                {
                    _controlDate = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть контрольну дату докуенту", maxLength: 10, initialValue: (Convert.ToDateTime(_initDate).AddDays(30)).ToShortDateString());
                    if (!TextServise.IsDateValid(_controlDate))
                    {
                        await Shell.Current.DisplayAlert("Додати новий елемент", $"Не вірно зазначена дата", "OK");
                        return;
                    }
                }

                string _investigator;
                var lastInvestigator = await App.DataBase.GetLastInvestigator(_criminalNumber);
                if (lastInvestigator != "empty")
                {
                    _investigator = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть слідчого", maxLength: 20, initialValue: lastInvestigator);
                }
                else
                {
                    _investigator = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть слідчого", maxLength: 20);
                }

                string _qualification;
                var lastQualification = await App.DataBase.GetLastQualification(_criminalNumber);
                if (lastQualification != "empty")
                {
                    _qualification = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть кваліфікацію", maxLength: 3, initialValue: lastQualification);
                }
                else
                {
                    _qualification = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть кваліфікацію", maxLength: 3);
                }

                List<string> _courts = new List<string>(Courts);
                string _court = await Shell.Current.DisplayActionSheet("Виберіть суд", "Cancel", null, _courts.ToArray());

                string _description = await Shell.Current.DisplayPromptAsync($"Додати новий елемент", $"Введіть опис", maxLength: 300);

                enforsementClass.CriminalNumber = _criminalNumber;
                enforsementClass.Type = _type;
                enforsementClass.InitDate = Convert.ToDateTime(_initDate);
                enforsementClass.ControlDate = Convert.ToDateTime(_controlDate);
                enforsementClass.Investigator = _investigator;
                enforsementClass.Qualification = _qualification;
                enforsementClass.Court = _court;
                enforsementClass.Description = _description;
                enforsementClass.Status = "на виконанні";
                var control = ItemControl(enforsementClass);
                if (control != null)
                {
                    enforsementClass = control;
                }
                await App.DataBase.SaveAsync(enforsementClass);
                SortState = "За порядковим номером";
                Clear();
                await Shell.Current.DisplayAlert("Додати новий елемент", $"Зарєстровано, порядковий номер {count}", "OK");
            }
            catch
            {
                await Shell.Current.DisplayAlert("Додати новий елемент", $"Помилка", "OK");
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
                if (item.Type == "Арешт майна")
                {
                    enforsementClassSoket.ControlDateSoket = "-";
                    enforsementClassSoket.Days = "-";
                }
                else if (item.Type == "Невідкладний обшук")
                {
                    enforsementClassSoket.ControlDateSoket = "-";
                    enforsementClassSoket.Days = "-";
                }
                else
                {
                    enforsementClassSoket.ControlDateSoket = item.ControlDate.ToShortDateString();
                    enforsementClassSoket.Days = (item.ControlDate - DateTime.Now).Days.ToString();
                }


                string _typeIcon = "";
                switch (enforsementClassSoket.Type)
                {
                    case "Арешт майна":
                        _typeIcon = "🔒";
                        break;
                    case "Тимчасовий доступ":
                        _typeIcon = "📃";
                        break;
                    case "Обшук":
                        _typeIcon = "🔎";
                        break;
                    case "Невідкладний обшук":
                        _typeIcon = "🚀";
                        break;
                    case "Огляд":
                        _typeIcon = "👁";
                        break;
                    case "Затримання з метою приводу":
                        _typeIcon = "🔗";
                        break;
                }

                enforsementClassSoket.TypeIcon = _typeIcon;
                _result.Add(enforsementClassSoket);
            }

            if (SortState == "За зростанням по даті винесення")
            {
                _result = _result.OrderBy(x => x.InitDate).ToList();
            }
            else if (SortState == "За спаданням по даті винесення")
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
            else if (SortState == "За порядковим номером")
            {
                _result = _result.OrderByDescending(x => x.Id).ToList();
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
            else if (SelectedTypeSearchPanel == "Затримання з метою приводу")
            {
                _result = _result.Where(x => x.Type == "Затримання з метою приводу").ToList();
            }
            else if (SelectedTypeSearchPanel == "Невідкладний обшук")
            {
                _result = _result.Where(x => x.Type == "Невідкладний обшук").ToList();
            }
            else if (SelectedTypeSearchPanel == "Огляд")
            {
                _result = _result.Where(x => x.Type == "Огляд").ToList();
            }

            if (SelectedQualificationSearchPanel != null)
            {
                var _subresult = _result.Where(x => x.Qualification == SelectedQualificationSearchPanel).ToList();
                if (_subresult.Count > 0)
                {
                    _result = _subresult;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Пошук", $"Не знайдено", "OK");
                    return;
                }
            }


            if (SelectedExecuteSearchPanel == "На виконанні")
            {
                _result = _result.Where(x => x.Status == "на виконанні").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Виконані")
            {
                _result = _result.Where(x => x.Status == "виконано").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Відмовлені судом")
            {
                _result = _result.Where(x => x.Status == "відмовлено").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Пропущені")
            {
                _result = _result.Where(x => x.Status == "пропущено").ToList();
            }
            else if (SelectedExecuteSearchPanel == "Прострочені")
            {
                _result = _result.Where(x => (x.ControlDate - DateTime.Now).TotalDays < 5).ToList();
                _result = _result.Where(x => x.Status == "на виконанні").ToList();
                _result = _result.Where(x => x.Type != "Арешт майна").ToList();
                _result = _result.Where(x => x.Type != "Невідкладний обшук").ToList();
            }

            if (SearchTextSearchPanel != null)
            {
                if (TextServise.IsNumberValid(SearchTextSearchPanel))
                {
                    var _subresult = _result.Where(x => x.CriminalNumber == SearchTextSearchPanel).ToList();
                    if (_subresult.Count > 0)
                    {
                        _result = _subresult;
                    }
                }
                else
                {
                    var _subresult = _result.Where(x => x.Description.Contains(SearchTextSearchPanel, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (_subresult.Count > 0)
                    {
                        _result = _subresult;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Пошук", $"Не знайдено", "OK");
                        return;
                    }

                }
            }


            if (_result.Count > 0)
            {
                foreach (var item in _result)
                {
                    Items.Add(item);
                }
            }

            return;
        }

        public async Task StartControl()
        {
            foreach (var item in await App.DataBase.GetAllAsync())
            {
                var r = ItemControl(item);
                if (r != null)
                {
                    await App.DataBase.UpdateAsync(r);
                }
            }
        }

        public EnforsementClass ItemControl(EnforsementClass _item)
        {
            bool isEnable = false;

            if (_item.Type == "Арешт майна")
            {
                if (_item.ControlDate != _item.InitDate)
                {
                    _item.ControlDate = _item.InitDate;
                    isEnable = true;
                }
            }

            if (_item.Type == "Невідкладний обшук")
            {
                if (_item.ControlDate != _item.InitDate)
                {
                    _item.ControlDate = _item.InitDate;
                    isEnable = true;
                }
            }

            if (String.IsNullOrWhiteSpace(_item.Description))
            {
                _item.Description = "-";
                isEnable = true;
            }

            if (String.IsNullOrWhiteSpace(_item.Qualification))
            {
                _item.Qualification = "-";
                isEnable = true;
            }

            if (String.IsNullOrWhiteSpace(_item.Investigator))
            {
                _item.Investigator = "-";
                isEnable = true;
            }

            if (isEnable == true)
            {
                return _item;
            }
            else
            {
                return null;
            }   
        }

        #endregion

    }
}
