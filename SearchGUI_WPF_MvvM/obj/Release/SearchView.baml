   M S B A M L   `   `   `  ���� M  ISearchGUI_WPF_MvvM, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null7 clr-namespace:SearchGUI_WPF_MvvMSearchGUI_WPF_MvvM  R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data #    SearchGUI_WPF_MvvM.SearchView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0ignorehttp://www.galasoft.ch/ignore     0src clr-namespace:SearchGUI_WPF_MvvM     ?data.clr-namespace:System.Data;assembly=System.Data       � Title$  Search��$	��410��$	��400�� � DataContext �� *SearchModel+   Locator �� Source# [   � 	Resources   ItemContextMenu m� Command �� Path �� RelativeSource �� Mode �� AncestorType  	Itemstyle	 �� 
TargetType
 �� Property �� Value��%�   &	       &	 w     7  �� .@�m� $��	More Info��
������� �� $ DataContext.MoreInfo� �� $ FindAncestor=�$ ListViewL��� "	 ��.�-   �� 
 �@�ContextMenu8   .���  � RowDefinitions �� $
��auto ��� $
��100* ��� $��* � � ColumnDefinitions �� $��* �.+��� $��0��$��0��$��Search Options��.����  �� Orientation$ Vertical=�$��5q�.+��� -    ��Name$ label1��$��Folder��$
��Left=�$	��Top=��� $ 
Horizontal=�.+��� -    ��Name$ TextBoxFolderLocation��$��Stretch=�$	��Top=�$��23��$	��295������ *FolderLocation+�� -    ��Name$ 	BtnBrowse��$��Browse��$��Stretch=�$��23��$��Right=�$��75������ *BrowseFileCommand+�� -   $ LabelSearchText��$��Search Text��$
��Left=�$	��Top=�$��24���� -   $ TextBoxSearchQuery��$��Stretch=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=����� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=��� $
��0,10q�._��� -    ��Name$ BtnSearchDocumentContents��$��Search document contents��$
��Left=�$	��Top=�$	��218�� d� 	IsChecked �� *LoadDocumentContents+� $��1��$��0��.+��� -   $��SearchResultsView��#��[ ���� $ SearchResults� �� View �� .P��� $
��File�� �� Width$
 Auto�� �� DisplayMemberBinding �� *
FileName+�� $
��Path��$
 Auto�� �� *FileLocation+�� $��Doc ID��$
 Auto�� �� *DocId+�� $��TF-IDF��$
 Auto�� �� *Score+�� $��Words��$
 Auto�� �� *DisplayMatchedWords+