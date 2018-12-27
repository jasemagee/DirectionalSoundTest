using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour {
    private readonly float _fireRate = 4f;
    private int _currentScore;
    private LastSound _lastSound;
    private float _nextFire;
    private SoundsTunnel[] _soundsTunnels;
    private bool _takenGuess = true;
    private bool _takenGuessRight;
    public Text Helper;
    public Text ScoreValue;

    // Start is called before the first frame update
    private void Start() {
        _soundsTunnels = FindObjectsOfType<SoundsTunnel>();
        Helper.text = string.Empty;
    }

    // Update is called once per frame
    private void Update() {
        if (_takenGuess && _lastSound != null) {
            if (!_takenGuessRight) {
                Helper.text = $"Wrong! It was {_lastSound.Direction} {_lastSound.Distance}.";
                Helper.color = Color.red;
            } else {
                Helper.text = $"Correct! It was {_lastSound.Direction} {_lastSound.Distance}.";
                Helper.color = Color.green;
            }
        }
        
        if (Time.time > _nextFire && _takenGuess) {
            var dist = (Distance) Random.Range(0, 2);
            var idx = Random.Range(0, _soundsTunnels.Length);

            var dir = _soundsTunnels[idx].Direction;
            //Debug.Log($"Fired {dir} at distance {dist}.");

            _soundsTunnels[idx].Fire(dist);



            _nextFire = Time.time + _fireRate;
            _takenGuess = false;
            _takenGuessRight = false;

            _lastSound = new LastSound {
                Distance = dist,
                Direction = dir
            };
        }

        ScoreValue.text = _currentScore.ToString();
    }

    public void ButtonPressed(string buttonName) {
        if (_lastSound == null) {
            return;
        }

        var direction = Direction.N;
        if (buttonName == "E") {
            direction = Direction.E;
        } else if (buttonName == "S") {
            direction = Direction.S;
        } else if (buttonName == "W") {
            direction = Direction.W;
        }

        if (_lastSound.Direction == direction) {
            _currentScore++;
            _takenGuessRight = true;
        }

        _takenGuess = true;
    }
}

public class LastSound {
    public Distance Distance { get; set; }
    public Direction Direction { get; set; }
}