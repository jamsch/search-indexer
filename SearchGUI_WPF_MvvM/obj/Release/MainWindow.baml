   M S B A M L   `   `   `  ���� E  ANlogViewer, Version=0.6.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:NlogViewer;assembly=NlogViewer
NlogViewer   SearchGUI_WPF_MvvM#   SearchGUI_WPF_MvvM.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0ignorehttp://www.galasoft.ch/ignore     =nlog,clr-namespace:NlogViewer;assembly=NlogViewer     $	��400��$	��560��  � Title$  Main Window�� � DataContext �� *Main+   Locator �� Source# [   � 	Resources  �� MergedDictionaries �� Source��  �� $ Skins/MainSkin.xaml?�%    .��� -    �Name$ 
LayoutRoot�� � RowDefinitions �� $
��auto ��� $
��auto ��� $��* �.+��� $��0��$��
Operations��.����  �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� -   	 ��Name$	 BtnOpenSearchWindow��$��Open search window��$	��130���� -   $	 BtnLoadIndex��$��
Load Index��$��90������ *LoadIndexCommand+�� -   $	 BtnSaveIndex��$��
Save Index��$��90������ *SaveIndexCommand+�� -   $	 BtnClearIndex��$��Clear Index��$��90������ *ClearIndexCommand+�� -   $	 BtnDumpDocumentCount��$��Dump document info��$	��130������ *DumpDocumentInfoCommand+�� $��1������ *WelcomeTitle+.���� $ Vertical=�.+��� -   
 ��Name$
 chkBackgroundSending��$'��!Send logs from background thread.�� d� 	IsChecked$ FalseY��� $��	Debug log��$��2��.��   NlogViewer.NlogViewer  -     Name$ LogCtrl��   
MaxNumLogs$	 300Q�   	TimeWidth$
 AUTO��   LoggerNameWidth$ 0��   
LevelWidth$
 auto��   ExceptionWidth$
 auto��   MessageWidth$
 auto��