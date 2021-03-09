﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BusinessLogic;
using System.Windows.Input;
using CommandHelper;

namespace WpfApp1.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        ICommand _getAllDataCommand;
        KFZModel _selectedKFZ;
        KFZCollectionModel _kfzCollModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public KFZModel SelectedKFZ
        {
            get { return _selectedKFZ; }

            set
            {
                _selectedKFZ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedKFZ"));
            }
        }


        public ObservableCollection<KFZModel> KFZListe
        {
            get;
            private set;
        }

        public MainWindowViewModel()
        {
            KFZListe = new ObservableCollection<KFZModel>();
            _kfzCollModel = new KFZCollectionModel();
            _kfzCollModel.KFZDataReady += _kfzCollModel_KFZDataReady;

            //List<KFZModel> tmp = km.KFZListe;

            //foreach (KFZModel kfzm in tmp)
            //{
            //    this.ocKFZ.Add(kfzm);
            //}
            SelectedKFZ = new KFZModel()
            {
                IsSelected = false,
                Idkfz = 1,
                Kennzeichen = "S-GH 65",
                Typ = "Limousine",
                Leistung = 123,
                FahrgestellNr = "FG 4245"
            };

            KFZListe.Add(SelectedKFZ);

            SelectedKFZ = new KFZModel()
            {
                IsSelected = false,
                Idkfz = 1,
                Kennzeichen = "BB-RB 4711",
                Typ = "SUV",
                Leistung = 204,
                FahrgestellNr = "4828345"
            };
            KFZListe.Add(SelectedKFZ);



        }

        private void _kfzCollModel_KFZDataReady(List<KFZModel> list)
        {
            KFZListe.Clear(); //Liste leeren.

            foreach (KFZModel kfzm in list) //Umverpacken von List -> ObservableCollection
            {
                KFZListe.Add(kfzm);
            }

            PropertyChanged(this, new PropertyChangedEventArgs("KFZListe")); //Event an die Oberfläche, dass 
            //in der KFZListe neue Daten enthalten sind.
        }

        public ICommand GetAllDataCommand
        {
            get
            {
                if (_getAllDataCommand == null)
                    _getAllDataCommand = new RelayCommand(c => GetAllData());
                return _getAllDataCommand;
            }
        }



        private void GetAllData()
        {
            _kfzCollModel.GetAllKFZFromDB();
        }
    }
}
