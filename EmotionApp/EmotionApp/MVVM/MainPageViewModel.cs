using Microsoft.ProjectOxford.Emotion;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Linq;
using EmotionApp.Models;

namespace EmotionApp.MVVM
{
    public class MainPageViewModel : BaseViewModel
    {
        private ImageCapture _imageCapture;
        private readonly EmotionServiceClient _client;
        private bool _canTakePicture;

        public MainPageViewModel()
        {
            _imageCapture = new ImageCapture();
            _client = new EmotionServiceClient("b9609c9cc0b848539f7f53a0f4ea5aad");

            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            _imageCapture.InitializeAsync();
            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            CanTakePicture = true;
        }

        private RelayCommand _takePictureCommand { get; set; }

        public ICommand TakePictureCommand
        {
            get
            {
                if (_takePictureCommand == null)
                    _takePictureCommand = new RelayCommand(TakePicture);

                return _takePictureCommand;
            }
        }

        public bool CanTakePicture
        {
            get { return _canTakePicture; }
            set
            {
                if (_canTakePicture == value)
                    return;

                _canTakePicture = value;
                ShoutAbout("CanTakePicture");
            }
        }

        private async void TakePicture()
        {
            CanTakePicture = false;
            var image = await _imageCapture.CaptureJpegImageAsync();
            
            await ProcessImageWithEmotionApiAsync(image);

            CanTakePicture = true;
        }

        private async Task ProcessImageWithEmotionApiAsync(WriteableBitmap image)
        {
            if (image == null)
                return;

            var file       = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync("lastImageCapture.jpg", Windows.Storage.CreationCollisionOption.OpenIfExists);
            var fileStream = await file.OpenStreamForReadAsync();
            var results    = await _client.RecognizeAsync(fileStream);

            ParseResults(results);
        }

        private void ParseResults(Emotion[] results)
        {
            if(results == null || results.Length == 0)
                return;

            var scores = results[0].Scores;

            var anger     = new EmotionScore("Anger"    , scores.Anger);
            var contemt   = new EmotionScore("Contempt" , scores.Contempt);
            var disgust   = new EmotionScore("Disgust"  , scores.Disgust);
            var fear      = new EmotionScore("Fear"     , scores.Fear);
            var happiness = new EmotionScore("Happiness", scores.Happiness);
            var neutral   = new EmotionScore("Neutral"  , scores.Neutral);
            var sadness   = new EmotionScore("Sadness"  , scores.Sadness);
            var surprise  = new EmotionScore("Surprise" , scores.Surprise);
        }

        public WriteableBitmap ContemptImage
        {
            get { return null; }
            set { }
        }
        public WriteableBitmap DisgustImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap FearImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap HappinessImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap NeutralImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap SadnessImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap SurpriseImage
        {
            get { return null; }
            set
            {
            }
        }
        public WriteableBitmap AngerImage
        {
            get { return null; }
            set
            {
            }
        }


    }
}
