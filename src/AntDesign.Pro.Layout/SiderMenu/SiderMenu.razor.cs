﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace AntDesign.Pro.Layout
{
    interface ISiderMenu : IBaseMenu
    {
        OneOf<string, RenderFragment> Logo { get; }
        int SiderWidth { get; }
        RenderFragment MenuExtraRender { get; }
        OneOf<bool, RenderFragment> CollapsedButtonRender { get; }
        BreakpointType Breakpoint { get; }
        EventCallback<MouseEventArgs> OnMenuHeaderClick { get; }
        bool Hide { get; }
        List<RenderFragment> Links { get; }
        EventCallback<string[]> OnOpenChange { get; }
    }

    public partial class SiderMenu : ISiderMenu
    {
        public string PrefixCls { get; } = "ant-pro";
        public string BaseClassName => $"{PrefixCls}-sider";
        [Parameter] public bool Collapsed { get; set; }
        [Parameter] public EventCallback<bool> HandleOpenChange { get; set; }
        [Parameter] public bool IsMobile { get; set; }
        [Parameter] public MenuDataItem[] MenuData { get; set; }
        [Parameter] public MenuMode Mode { get; set; } = MenuMode.Inline;
        [Parameter] public EventCallback<bool> OnCollapse { get; set; }
        [Parameter] public string[] OpenKeys { get; set; }
        [Parameter] public OneOf<string, RenderFragment> Logo { get; set; }
        [Parameter] public int SiderWidth { get; set; } = 208;
        [Parameter] public OneOf<bool, RenderFragment> CollapsedButtonRender { get; set; }
        [Parameter] public BreakpointType Breakpoint { get; set; } = BreakpointType.Lg;
        [Parameter] public bool Hide { get; set; }
        [Parameter] public List<RenderFragment> Links { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnMenuHeaderClick { get; set; }
        [Parameter] public EventCallback<string[]> OnOpenChange { get; set; }
        [Parameter] public RenderFragment MenuExtraRender { get; set; }
        [Parameter]
        public SiderTheme SiderTheme
        {
            get
            {
                return NavTheme.Name switch
                {
                    "light" => SiderTheme.Light,
                    "dark" => SiderTheme.Dark,
                    _ => SiderTheme.Light
                };
            }
            set => NavTheme = value == SiderTheme.Light ? MenuTheme.Light : MenuTheme.Dark;
        }

        async Task CollapseSider(bool collapse)
        {
            if (!IsMobile)
            {
                await OnCollapse.InvokeAsync(collapse);
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetClassMap();
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add(BaseClassName)
                .If($"{BaseClassName}-fixed", () => FixSiderbar)
                .If($"{BaseClassName}-layout-{Layout}", () => !IsMobile)
                .If($"{BaseClassName}-light", () => NavTheme == MenuTheme.Light);
        }
    }
}
