using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorArena
{

    
    class MusicMan : Game
    {
        private const string SOUNDS_PATH = "sounds/";
        private const float SOUND_VOLUME = 0.5f;
        private Dictionary<string, SoundEffect> m_sounds;
        private GraphicsDeviceManager graphics;
        private SoundEffectInstance m_moveSoundInstance;
        private SoundEffectInstance m_actionSoundInstance;

        public MusicMan()
        {
            graphics = new GraphicsDeviceManager(this);
            m_sounds = new Dictionary<string, SoundEffect>();
            Content.RootDirectory = "Content";
            initializeSounds();
        }

        private void initializeSounds() {
            movements();
            actions();
        }

        public void playMoveSound(string sound)
        {
            playMoveSound(sound, 1);
        }

        public void playMoveSound(string sound, int loop_for)
        {
            playSound(sound, loop_for, m_moveSoundInstance);
        }

        public void playActionSound(string sound)
        {
            playActionSound(sound, 1);
        }

        public void playActionSound(string sound, int loop_for)
        {
            playSound(sound, loop_for, m_actionSoundInstance);
        }

        private void playSound(string sound, int loop_for, SoundEffectInstance soundInstance) {
            SoundEffect soundEffect;
            m_sounds.TryGetValue(sound, out soundEffect);

            if (soundEffect != null) {
                if (soundInstance != null) {
                    soundInstance.Stop();
                }

                soundInstance = soundEffect.CreateInstance();
                soundInstance.IsLooped = loop_for > 1;
                if (loop_for > 1) {
                    //TODO
                    
                }
                soundInstance.Volume = SOUND_VOLUME;
                soundInstance.Play();
            } else {
                Console.Out.WriteLine("Sound '" + sound + "' hasn't been found");
            }
        }

        private void movements() {
            m_sounds.Add("grass01", Content.Load<SoundEffect>(SOUNDS_PATH + "StepGrass01"));
            m_sounds.Add("dirt01", Content.Load<SoundEffect>(SOUNDS_PATH + "StepDirt01"));
            m_sounds.Add("sand01", Content.Load<SoundEffect>(SOUNDS_PATH + "StepSand01"));
            m_sounds.Add("stone01", Content.Load<SoundEffect>(SOUNDS_PATH + "StepStone01"));
        }

        private void actions() {
            m_sounds.Add("axe01", Content.Load<SoundEffect>(SOUNDS_PATH + "HitAxe01"));
            m_sounds.Add("club01", Content.Load<SoundEffect>(SOUNDS_PATH + "HitClub01"));
            m_sounds.Add("dagger01", Content.Load<SoundEffect>(SOUNDS_PATH + "HitDagger01"));
            m_sounds.Add("sword01", Content.Load<SoundEffect>(SOUNDS_PATH + "HitSword01"));
        }
    }
}
