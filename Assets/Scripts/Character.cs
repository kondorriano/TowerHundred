using UnityEngine;

public class CharAnimation
{
    public enum Type
    {
        Single,
        Loop
    };

    public struct Frame
    {
        public int msecs;
        public int velx;
        public int vely;

        public Frame(int _msecs, int _velx, int _vely)
        {
            msecs = _msecs;
            velx = _velx;
            vely = _vely;
        }
    }

    public Frame[] frames;
    public Type type;
}

public class Character : MonoBehaviour
{
    private float posx;
    private float posy;

    private CharAnimation currAnimation;
    public int currFrame;
    public int currTime;

    public CharAnimation CurrentAnimation
    {
        get
        {
            return currAnimation;
        }
        set
        {
            currAnimation = value;
            currFrame = 0;
            currTime = 0;
        }
    }

    void Start ()
    {
        CharAnimation test = new CharAnimation();
        test.type = CharAnimation.Type.Loop;
        test.frames = new CharAnimation.Frame[2];
        test.frames[0] = new CharAnimation.Frame(200, 1000, 0);
        test.frames[1] = new CharAnimation.Frame(100, 3000, 0);

        currAnimation = test;
    }
	
	void FixedUpdate()
    {
        int fixedTime = (int)(Time.fixedDeltaTime * 1000.0f);
        if (currAnimation != null && currAnimation.frames.Length > 0)
        {
            currTime += fixedTime;

            while(currFrame < currAnimation.frames.Length)
            {
                CharAnimation.Frame curr = currAnimation.frames[currFrame];
                posx += curr.velx / (float) fixedTime;
                posy += curr.vely / (float) fixedTime;

                if (curr.msecs <= currTime)
                {
                    currTime -= curr.msecs;
                    currFrame++;

                    if (currAnimation.type == CharAnimation.Type.Loop && currFrame == currAnimation.frames.Length)
                    {
                        currFrame = 0;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        transform.position = new Vector3(posx / 1000.0f, posy / 1000.0f, 0);
	}
}
