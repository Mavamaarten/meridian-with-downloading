﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Meridian.ViewModel;

namespace Meridian.Layout
{
    public class PageBase : Page
    {
        /// <summary>
        /// Content property
        /// </summary>
        public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(List<object>), typeof(PageBase), new PropertyMetadata(new List<object>()));

        public new List<object> Content
        {
            get { return (List<object>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Header property
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(object), typeof(PageBase), new PropertyMetadata(default(object)));

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Header menu items property
        /// </summary>
        public static readonly DependencyProperty HeaderMenuItemsProperty = DependencyProperty.Register(
            "HeaderMenuItems", typeof(List<MenuItem>), typeof(PageBase), new PropertyMetadata(new List<MenuItem>()));

        public List<MenuItem> HeaderMenuItems
        {
            get { return (List<MenuItem>)GetValue(HeaderMenuItemsProperty); }
            set { SetValue(HeaderMenuItemsProperty, value); }
        }

        /// <summary>
        /// Navigation context
        /// </summary>
        public NavigationContext NavigationContext { get; set; }


        public PageBase()
        {
            Style = (Style)Application.Current.Resources["PageBaseStyle"];

            HeaderMenuItems = new List<MenuItem>();
            Content = new List<object>();

            NavigationContext = new NavigationContext();

            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var vm = DataContext as ViewModelBase;
            if (vm != null)
                vm.Activate();

            OnNavigatedTo();
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var vm = DataContext as ViewModelBase;
            if (vm != null)
                vm.Deactivate();

            OnNavigatedFrom();
        }

        public virtual void OnNavigatedTo()
        {

        }

        public virtual void OnNavigatedFrom()
        {

        }
    }

    public sealed class NavigationContext
    {
        private readonly Dictionary<string, object> _parameters;

        public Dictionary<string, object> Parameters
        {
            get { return _parameters; }
            set
            {
                if (value == null)
                    return;

                foreach (var kp in value)
                {
                    _parameters.Add(kp.Key, kp.Value);
                }
            }
        }

        public NavigationContext()
        {
            _parameters = new Dictionary<string, object>();
        }
    }
}
