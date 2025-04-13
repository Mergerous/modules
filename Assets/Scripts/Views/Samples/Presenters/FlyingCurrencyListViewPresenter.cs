using System;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Modules.UI.Views;
using UnityEngine;

namespace Common.Views
{
    [UsedImplicitly]
    public sealed class FlyingCurrencyListViewPresenter 
    {
        private readonly View view;
        private readonly List<FlyingCurrencyItemViewPresenter> instances = new();
        private readonly Func<FlyingCurrencyItemViewPresenter> factory;

        public FlyingCurrencyListViewPresenter(View view, Func<FlyingCurrencyItemViewPresenter> factory)
        {
            this.view = view;
            this.factory = factory;
        }

        public void SetImage(Sprite sprite)
        {
            foreach (FlyingCurrencyItemViewPresenter instance in instances)
            {
                instance.SetImage(sprite);
            }
        }

        public Sequence DoMove(int count, Vector3 sourcePosition, Vector3 destinationPosition)
        {
            Sequence sequence = DOTween.Sequence();
            
            for (int i = 0; i < count; i++)
            {
                ListElement listElement = view.GetElement<ListElement>("list");
                View itemView = listElement.CreateInstance("default");
                FlyingCurrencyItemViewPresenter presenter = factory();
                presenter.Initialize(itemView);
                instances.Add(presenter);
                
                sequence.Join(presenter.DoMove(sourcePosition, destinationPosition).OnComplete(() =>
                {
                    if (instances.Remove(presenter))
                    {
                        presenter.Dispose();
                    }
                    
                    listElement.RemoveInstance(itemView);
                }));
            }

            return sequence;
        }
    }
}
