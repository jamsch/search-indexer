   M S B A M L   `   `   `  ����E  ANlogViewer, Version=0.6.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:NlogViewer;assembly=NlogViewer
NlogViewer  5   	    SearchGUI_WPF_MvvM#   SearchGUI_WPF_MvvM.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   =nlog,clr-namespace:NlogViewer;assembly=NlogViewer     5   	   $	��350��5	   	   $	��550��5
   	     � Title$  Main Window��5   	    � DataContext 5   	   �� *Main+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Skins/MainSkin.xaml?�6%   %    5      .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   $��
Operations��6    .��5      ��  �� Orientation$ Vertical=�6   .+�5       �� $ 
Horizontal=�6   .+�5!      �� -   5        	 ��Name$	 BtnOpenSearchWindow��5!      $��Open search window��6:   $	��130��6W   6c   �� 5"      -   5        $	 BtnStartWebServer��5"      $��Start web server��68   $	��130��6T   �� 5#      -   5        $	 BtnOpenIndexWindow��5#      $��Open Indexing window��69   $	��130��6X   5$      5%      5&      �� 5'   
   $��1��6   ��6    �� *WelcomeTitle+.��5(      �� $ Vertical=�6   .+�5)      �� -   5        
 ��Name$
 chkBackgroundSending��5)      $'��!Send logs from background thread.��69    d� 	IsChecked$ FalseY�6e   5*      5+      �� 5-   
   $��	Debug log��6   $��2��6&   .��5.         NlogViewer.NlogViewer  -   5          Name$ LogCtrl��5.         	TimeWidth$
 AUTO��6/      LoggerNameWidth$ 0��6@      
LevelWidth$
 auto��6T      ExceptionWidth$
 auto��6f      MessageWidth$
 auto��6|   5/      52      53      