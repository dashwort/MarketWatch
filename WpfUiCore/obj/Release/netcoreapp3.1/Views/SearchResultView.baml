   M S B A M L   `   `   `  ���� D  @WpfUiCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfUiCore.ViewsWpfUiCore.Views  &     WpfUiCore.Views.SearchResultView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     /localclr-namespace:WpfUiCore.Views     ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  ���	#FF3F3F46  J� OpacityMask
  �   �
��� ���.���  � ColumnDefinitions �� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��20 ��� $��23 ��� $��* ��� $��20 � � 	Resources  �� 
TargetType �� Property �� Value A� Columns �� Triggers	 P� Property
 P� Value��%&  '��       '�� �     �� " ��.���  �@�ToolTip A� $ 1�� P� 	 �@��AlternationIndex$
 0��. ���  � ��
 �����P� 	 �@��AlternationIndex$
 1��. ���  � �� �#f1f7fa�� " ��.���  � �� �#f1f7fa��  � ��$ 80����  � ��$ 10q�.+�� $��1��$��1��
������� �� $��0.63* ��� $��0.27* �.+��� -    ��Name$ SearchString��$��0��$��1��  Caliburn.Micro.Message   Attach$V P[Event PreviewKeyDown] = [Action SearchString_PreviewKeyDown($this, $eventArgs)]��.���� $��1�� �� 
IsEditable$
 True�� �� 
IsReadOnly$
 True�� �� Text$ Symbol������  �� Path$ SearchOptions� �� SelectedValue �� $ SearchComboText��� $��1��$��2��$��1�� �� AlternationCount$ 2�����#f1f7fa �� SelectionMode$ Single=�$V P[Event MouseDoubleClick] = [Action ListView_MouseDoubleClick($this, $eventArgs)]������ $ Results� �� SelectedItem �� $ SelectedResult����� .R��� ���#CCCCCC$��1q�._��  �� $��50 ��� $��30 ��� $��40 ��� $	��400 ��� $��30 � �� $��30 ��� $��30 ��� $��30 �.+��� $��14(�$��1��$��4��$
��Bold"����� *DisplaySymbol+�� $��1��$��1��$��4��$��Light"����� *Description+��  �� Orientation$ 
Horizontal=�$��2��$��1��$��4�� ��%M   '��       �� " ��.���  � ��$ 0,2,2,0q���  � ��$ 5,0,0,2q�.+��� $��
UltraLight"����� *Symbol+�� $��
UltraLight"����� *Type+