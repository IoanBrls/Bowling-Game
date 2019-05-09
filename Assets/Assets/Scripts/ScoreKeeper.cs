using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public List<BowlingFrame> frames = new List<BowlingFrame>();
    public List<BowlingFrame> frames2 = new List<BowlingFrame>();

    public int _frame;
    public int _Down;
    public int _frameBall = 0;
    public int _Score;
    public int _frame2;
    public int _Down2;
    public int _frameBall2 = 0;
    public int _Score2;
    public bool strike;
    public bool spare;

    public AudioClip Fools;
    public AudioClip StrikeSound;
    public AudioClip SpareSound;
    public AudioClip WinningSound;
    private float volume = 0.5f;
    private AudioSource SFX;
    
    public GameObject Player;
    public Transform PlayerSpawn;

    public bool plr1;
    public bool plr2;
    public int winner;
    public bool cheat;

    void Start()
    {
        frames.Add(new BowlingFrame(0));
        frames2.Add(new BowlingFrame(0));
        strike = false;
        spare = false;
        plr1 = true;
        plr2 = false;
        cheat = false;
    }

    void Awake()
    {
        SFX = GetComponent<AudioSource>();
    }

    int getDownPins()
    {
        int down = 0;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pin"))
        {
            if(g.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
            {
                Matrix4x4 m = g.transform.localToWorldMatrix;
                Vector3 uv = m.MultiplyVector(Vector3.up).normalized;

                if (uv.y < 0.7)
                {
                    down += 1;
                    g.GetComponent<MeshRenderer>().enabled = false;
                    g.GetComponent<Collider>().enabled = false;

                }
                continue;
            }
            if (g.transform.position.y <0 || g.transform.position.z >1 || g.transform.position.z < -1)
            {
                down += 1;
                g.GetComponent<MeshRenderer>().enabled = false;
                g.GetComponent<Collider>().enabled = false;
                continue;
            }
        }
        return down;
    }

    public void UpdateScore(object ball)
    {
        if (plr1)
        {
            _Down = getDownPins();

            if(_Down==0)
            {
                SFX.PlayOneShot(Fools, volume);
            }

            if(_Down > 10)
            {
                cheat = true;
                _Down = 0;
            }
            else
            {
                cheat = false;
            }

            BowlingFrame bf = frames[frames.Count - 1].Addscore(_frameBall, _Down);
            _frameBall += 1;

            if(frames[frames.Count - 1].strike)
            {
                strike = true;
                SFX.PlayOneShot(StrikeSound, volume);
            }
            else
            {
                strike = false;
            }

            if (frames[frames.Count - 1].spare)
            {
                spare = true;
                SFX.PlayOneShot(SpareSound, volume);
            }
            else
            {
                spare = false;
            }

            if (bf != null)
            {
                frames.Add(bf);
                NewFrame();
            }
        }
        else if (plr2)
        {
            _Down2 = getDownPins();
            if (_Down2 == 0)
            {
                SFX.PlayOneShot(Fools, volume);
            }

            if(_Down2>10)
            {
                cheat = true;
                _Down2 = 0;
            }
            else
            {
                cheat = false;
            }

            BowlingFrame bf = frames2[frames2.Count - 1].Addscore(_frameBall2, _Down2);
            _frameBall2 += 1;

            if(frames2[frames2.Count - 1].strike)
            {
                    strike = true;
                    SFX.PlayOneShot(StrikeSound, volume);

            }
            else
            {
                    strike = false;
            }

            if (frames2[frames2.Count - 1].spare)
            {
                    spare = true;
                    SFX.PlayOneShot(SpareSound, volume);
            }
            else
            {
                    spare = false;
            }

            if (bf != null)
            {
                frames2.Add(bf);
                NewFrame();
            }
        }
    }

    public void NewFrame()
    {
        if(plr1)
        {
            _frameBall = 0;
            _frame = frames.Count-1;
            gameObject.SendMessage("ResetFrame", SendMessageOptions.RequireReceiver);
            _Score = BowlingFrame.Score(frames);
            plr1 = false;
            plr2 = true;
        }
        else if(plr2)
        {
            _frameBall2 = 0;
            _frame2 = frames2.Count-1;
            gameObject.SendMessage("ResetFrame", SendMessageOptions.RequireReceiver);
            _Score2 = BowlingFrame.Score(frames2);
            plr1 = true;
            plr2 = false;
        }
        if (_frame == 10 && _frame2 == 10)
        {
            SFX.PlayOneShot(WinningSound, 1.0f);
            if (_Score>_Score2)
            {
                winner = 1;
            }
            else if (_Score<_Score2)
            {
                winner = 2;
            }
            else
            {
                winner = 3;
            }
        }
        ResetPlayerPosition();
        
    }

    public void ResetPlayerPosition()
    {
        Player.transform.position = PlayerSpawn.position;
        Player.transform.rotation = PlayerSpawn.rotation;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}

public class BowlingFrame
{
    int Score1 = 0;
    int Score2 = 0;
    int Carry;
    public bool strike = false;
    public bool spare = false;
    

    public BowlingFrame(int carries)
    {
        Carry = carries;
    }

    public BowlingFrame Addscore(int ball, int score)
    {
        if (ball == 0)
        {
            Score1 = Mathf.Max(score,0);
            if (score == 10)
            {
                strike = true;
                return new BowlingFrame(2);
            }
            return null;
        }
        else
        {
            Score2 = score - Score1;
            if (Score1 + Score2 == 10)
            {
                spare = true;
                return new BowlingFrame(1);
            }
            return new BowlingFrame(0);
        }
    }

    public static int Score(IEnumerable<BowlingFrame> frames)
    {
        int score = 0;
        foreach (BowlingFrame f in frames)
        {
            score += f.Score1;
            score += f.Score2;
            if (f.Carry > 0) score += f.Score1;
            if (f.Carry > 1) score += f.Score2;
        }

        return score;
    }
    
}