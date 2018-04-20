using UnityEngine;
using System.Collections.Generic;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}

public class PlayerScript : MonoBehaviour
{
    public AnimationClip[] anmations;
    protected Animator anim;
    protected AnimatorOverrideController animatorOverrideController;
    protected AnimationClipOverrides clipOverrides;
    protected int weaponIndex;
    //int start, main, end, def;
    //AnimatorStateInfo stateInfo;
    void Start()
    {/*
        start = Animator.StringToHash("Base.start");
        main = Animator.StringToHash("Base.main");
        end = Animator.StringToHash("Base.end");
        def = Animator.StringToHash("Base.default");*/

        anim = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

        clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(clipOverrides);
    }

    // Update is called once per frame
    void Update()
    {
        //stateInfo = anim.GetCurrentAnimatorStateInfo(0);

    }
    public void Play()
    {
        // if (def == stateInfo.fullPathHash)
        switch (anim.GetInteger("setAnimation"))
        {
            case 1:
                {
                    clipOverrides["start_1"] = anmations[0];
                    clipOverrides["main_1"] = anmations[1];
                    clipOverrides["end_1"] = anmations[2];
                    animatorOverrideController.ApplyOverrides(clipOverrides);
                    anim.Play("start");
                    break;
                }
            case 2:
                {
                    clipOverrides["start_1"] = anmations[0];
                    clipOverrides["main_1"] = anmations[0];
                    clipOverrides["end_1"] = anmations[0];
                    animatorOverrideController.ApplyOverrides(clipOverrides);
                    anim.Play("start");
                    break;
                }
            case 3:
                {
                    //anim.Play("start2");
                    clipOverrides["start_1"] = anmations[3];
                    clipOverrides["main_1"] = anmations[3];
                    clipOverrides["end_1"] = anmations[3];
                    animatorOverrideController.ApplyOverrides(clipOverrides);
                    anim.Play("start");
                    break;
                }
            default:
                {
                    clipOverrides["start_1"] = anmations[0];
                    clipOverrides["main_1"] = anmations[0];
                    clipOverrides["end_1"] = anmations[0];
                    animatorOverrideController.ApplyOverrides(clipOverrides);
                    anim.Play("start");
                    break;
                }
        }

    }
    public void Default()
    {
        anim.Play("default");
    }
    public void Animation1()
    {
        anim.SetInteger("setAnimation", 1);
    }
    public void Animation2()
    {
        anim.SetInteger("setAnimation", 2);
    }
    public void Animation3()
    {
        anim.SetInteger("setAnimation", 3);
    }
}
