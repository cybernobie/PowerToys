﻿// Copyright (c) Brice Lambson
// The Brice Lambson licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.  Code forked from Brice Lambson's https://github.com/bricelam/ImageResizer/

using System;
using System.Text;
using System.Windows;
using ImageResizer.Models;
using ImageResizer.Properties;
using ImageResizer.Utilities;
using ImageResizer.ViewModels;
using ImageResizer.Views;

namespace ImageResizer
{
    public partial class App : Application, IDisposable
    {
        static App()
        {
            Console.InputEncoding = Encoding.Unicode;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Fix for .net 3.1.19 making Image Resizer not adapt to DPI changes.
            NativeMethods.SetProcessDPIAware();

            if (PowerToys.GPOWrapperProjection.GPOWrapper.GetConfiguredImageResizerEnabledValue() == PowerToys.GPOWrapperProjection.GpoRuleConfigured.Disabled)
            {
                /* TODO: Add logs to ImageResizer.
                 * Logger.LogWarning("Tried to start with a GPO policy setting the utility to always be disabled. Please contact your systems administrator.");
                 */
                Environment.Exit(0); // Current.Exit won't work until there's a window opened.
                return;
            }

            var batch = ResizeBatch.FromCommandLine(Console.In, e?.Args);

            // TODO: Add command-line parameters that can be used in lieu of the input page (issue #14)
            var mainWindow = new MainWindow(new MainViewModel(batch, ImageResizerSettings.Default));
            mainWindow.Show();

            // Temporary workaround for issue #1273
            BecomeForegroundWindow(new System.Windows.Interop.WindowInteropHelper(mainWindow).Handle);
        }

        private static void BecomeForegroundWindow(IntPtr hWnd)
        {
            NativeMethods.INPUT input = new NativeMethods.INPUT { type = NativeMethods.INPUTTYPE.INPUT_MOUSE, data = { } };
            NativeMethods.INPUT[] inputs = new NativeMethods.INPUT[] { input };
            _ = NativeMethods.SendInput(1, inputs, NativeMethods.INPUT.Size);
            NativeMethods.SetForegroundWindow(hWnd);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            GC.SuppressFinalize(this);
        }
    }
}
