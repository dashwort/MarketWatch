   M S B A M L   `   `   `  ���� D  @WpfUiCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfUiCore.ViewsWpfUiCore.Views  #    WpfUiCore.Views.StockListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:WpfUiCore.Views     
��� � �.���   � ColumnDefinitions  �� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��20 ��� $
��auto ��� $��* ��� $��20 � � 	Resources  �� 
TargetType �� Property �� Value A� Columns �� Triggers P� Property	 P� Value��%D  '��       '�� �     �� " ��.���  �@�ToolTip A� $ 1��.+��� ���� *
Quote.Pc+ P�  �@��AlternationIndex$	 0��. ���  � ��
 �����P�  �@��AlternationIndex$	 1��. ���  � �� �#f1f7fa�� " ��.���  � �� �#f1f7fa��  � ��$ 80����  � ��$ 10q�.+��� $��1��$��2��$��1��
 �� AlternationCount$
 2�����#f1f7fa �� SelectionMode$ Single=�����  �� Path$ Stocks� �� SelectedItem �� $ SelectedStock����� .R��� ���#CCCCCC$��1q�._��   �� $��50 ��� $��30 ��� $��40 ��� $	��400 ��� $��30 � �� $��30 ��� $��30 ��� $��30 �.+��� $��../icons/email_icon.png��$��4��$��Center=�$��5q��� $��14(�$��1��$��4��$
��Bold"����� *Name+�� $��1��$��1��$��4��$��Light"����� *
Exchange+��  �� Orientation$ 
Horizontal=�$��2��$��1��$��4�� ��%M   '��       �� " ��.���  � ��$ 0,2,2,0q���  � ��$ 5,0,0,2q�.+��� $��
UltraLight"����� *Ticker+�� $��
UltraLight"����� *FinnhubIndustry+�� $��
UltraLight"����� *
Quote.Pc+