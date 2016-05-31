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

        private EmotionScore _surprise;
        private EmotionScore _anger;
        private EmotionScore _contemt;
        private EmotionScore _disgust;
        private EmotionScore _fear;
        private EmotionScore _happiness;
        private EmotionScore _neutral;
        private EmotionScore _sadness;

        public MainPageViewModel()
        {
            _imageCapture = new ImageCapture();
            _client = new EmotionServiceClient("[Your client secret goes here]");

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
            
            
            _anger     = new EmotionScore("Anger"    , scores.Anger);
            _contemt   = new EmotionScore("Contempt" , scores.Contempt);
            _disgust   = new EmotionScore("Disgust"  , scores.Disgust);
            _fear      = new EmotionScore("Fear"     , scores.Fear);
            _happiness = new EmotionScore("Happiness", scores.Happiness);
            _neutral   = new EmotionScore("Neutral"  , scores.Neutral);
            _sadness   = new EmotionScore("Sadness"  , scores.Sadness);
            _surprise  = new EmotionScore("Surprise" , scores.Surprise);
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
        public EmotionScore AngerImage
        {
            get { return new EmotionScore("Anger", 66.7f); }
            set
            {
            }
        }


    }
}
