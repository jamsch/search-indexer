   M S B A M L   `   `   `  ����  SearchGUI_WPF_MvvM#    SearchGUI_WPF_MvvM.SearchView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	     � DataContext  5   	   �� *SearchModel+   Locator �� Source# [  .��5
      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6    $��Search Options��60   .��5      ��  �� Orientation$ Vertical=�6   $��5q�60   .+�5      �� -   5         ��Name$ label1��5      $��Folder��6(   $
��Left=�69   $	��Top=�6U   �� 5      $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ TextBoxFolderLocation��5      $��Stretch=�6=   $	��Top=�6[   $��23��6s   $	��365��6   �� 5      -   5         ��Name$ 	BtnBrowse��5      $��Browse��60   $��Stretch=�6A   $��23��6]   $��Right=�6�   $��75��6�   5      �� 5      -   5        $ LabelSearchText��5      $��Search Text��61   $
��Left=�6G   $	��Top=�6b   $��24��6z   �� 5      -   5        $ TextBoxSearchQuery��5      $��Stretch=�66   $��23��6T    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   �� 5!      $��10q�6   ._�5"      �� -   5        	 ��Name$	 BtnSearchDocumentContents��5"      $&�� Search through document contents��6B   $
��Left=�6m   $	��Top=�6�   $	��218��6�   5#      �� 5$      -   5        $ 	btnSearch��5$      $��Search��6,   $
��Left=�6=   $	��Top=�6Y   $��75��6q   6|   5'      5(      �� 5)   
   $��1��6   $��0��6    $��Filter��60   .��5+      �� $ 
Horizontal=�6   .+�5,      (  "System.Windows.Controls.DatePicker  $
��Left=�6   $	��Top=�68     5-      $
��Left=�6   $	��Top=�68   5.      5/      �� 51   
   $��2��6   $��0��6    $��Additional Settings��60   .��52      �� $ Vertical=�6   $��5q�60   .+�53      �� -   5        $	 	checkBox1��53      $��Search file names��6.   $
��Left=�6J   $	��Top=�6e   $	��209��6}   
 d� 	IsChecked$

 TrueY�6�   54      55      56      57      