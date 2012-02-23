using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAzure.Samples.Phone.Storage;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace PhoneApp2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.LoadBlobContainerCommand = new RelayCommand(LoadBlobContainerExecute);
            this.Containers = new ObservableCollection<ICloudBlobContainer>();
        }

        public RelayCommand LoadBlobContainerCommand { get; private set; }
        public ObservableCollection<ICloudBlobContainer> Containers { get; private set; }

        private void LoadBlobContainerExecute()
        {
            var blobClient = CloudStorageClientResolverAccountAndKey
                .DevelopmentStorageAccountResolver
                .CreateCloudBlobClient();
            this.Containers.Clear();
            blobClient.ListContainersAsObservable()
                .Subscribe(
                    o => this.Containers.AddRange(o),
                    ex => MessageBox.Show(ex.Message),
                    () => MessageBox.Show("Š®—¹"));
        }


    }

    public static class CloudBlobClientExtensions
    {
        public static IObservable<IEnumerable<ICloudBlobContainer>> ListContainersAsObservable(this ICloudBlobClient self)
        {
            return Observable.Defer(() =>
            {
                var o = new AsyncSubject<IEnumerable<ICloudBlobContainer>>();
                self.ListContainers(response =>
                {
                    if (!response.Success)
                    {
                        o.OnError(new InvalidOperationException(response.ErrorMessage));
                        return;
                    }
                    o.OnNext(response.Response);
                    o.OnCompleted();
                });
                return o;
            });
        }
    }

    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> items)
        {
            foreach (var i in items)
            {
                self.Add(i);
            }
        }
    }
}