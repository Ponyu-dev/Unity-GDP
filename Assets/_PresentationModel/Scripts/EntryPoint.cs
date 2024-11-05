using System;
using _PresentationModel.Scripts.HeroInfo;
using _PresentationModel.Scripts.LevelUp;
using _PresentationModel.Scripts.UI;
using Lessons.Architecture.PM;
using Popups.Samples.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _PresentationModel.Scripts
{
    [Serializable]
    public class EntryPoint : PopupEntryPoint
    {
        [SerializeField] private StatView statView;
        
        [BoxGroup("Hero")]
        [BoxGroup("Hero/Ork")]
        [SerializeField] private Character ork;
        [BoxGroup("Hero/Ork")] 
        [SerializeField] private HeroInfoView orkInfoView;
        
        [BoxGroup("Hero/Paladin")]
        [SerializeField] private Character paladin;
        [BoxGroup("Hero/Paladin")]
        [SerializeField] private HeroInfoView paladinInfoView;
        
        [BoxGroup("Hero/Mage")]
        [SerializeField] private Character mage;
        [BoxGroup("Hero/Mage")]
        [SerializeField] private HeroInfoView mageInfoView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(statView);
            
            builder.Register<LevelUpPresenter>(Lifetime.Scoped)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<HeroInfoPresenter>(Lifetime.Scoped)
                .WithParameter(ork)
                .WithParameter(orkInfoView)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<HeroInfoPresenter>(Lifetime.Scoped)
                .WithParameter(paladin)
                .WithParameter(paladinInfoView)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<HeroInfoPresenter>(Lifetime.Scoped)
                .WithParameter(mage)
                .WithParameter(mageInfoView)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}