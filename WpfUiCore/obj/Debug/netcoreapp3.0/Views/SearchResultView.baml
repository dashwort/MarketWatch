   M S B A M L   `   `   `  ����D  @WpfUiCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfUiCore.ViewsWpfUiCore.Views  5      &     WpfUiCore.Views.SearchResultView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WpfUiCore.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      ���	#FF3F3F46  J� OpacityMask
  �   �
��� ���.��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��23 �6   �� 5      $��* �6   �� 5      $��20 �6    � 	Resources 5   
    �� 
TargetType �� Property �� Value A� Columns �� Triggers	 P� Property
 P� Value��%[  '��       '�� �    �� 5      " ��6   .�5      ��  �@�ToolTip 5      A� 5      $ 1��6&   .+�5      �� ��6(   �� *Description+5       5"      �� 5#       � ��$ Stretch=�6?    5%      P� 5&      	 �@��AlternationIndex$
 0��6H   . �5'      ��  � ��
 �����67   5(      P� 5)      	 �@��AlternationIndex$
 1��6H   . �5*      ��  � �� �#f1f7fa67   5+      5-      �� 5/      " ��6   .�50      ��  � �� �#f1f7fa6/   �� 51       � ��$ 80��6*   �� 52       � ��$ 10q�6+   53      54      .+�56   
   � $��1��6   $��1��57      
������� 59      �� 5:      $��0.63* �6#   �� 5;      $��0.27* �6#   .+�5?      �� -   5         ��Name$ SearchString��5@      $��0��5?      $��1��5A        Caliburn.Micro.Message   Attach$� �[Event PreviewKeyDown] = [Action SearchString_PreviewKeyDown($this, $eventArgs)];                          [Event TextChanged] = [Action SearchString_TextChanged($this, $eventArgs)]��5B      .��5D      5E      �� 5F      $��1��6    �� 
IsEditable$
 True��5I       �� 
IsReadOnly$
 True��6)    �� Text$ Symbol��6;   ��5G      ��  �� Path$ SearchOptions� �� SelectedValue 5H      �� $ SearchComboText�5K      5M      �� 5P   
   $��1��6   $��2��5Q      $��1��6     �� AlternationCount$ 2��5S      ���#f1f7fa �� SelectionMode$ Single=�5V      $V P[Event MouseDoubleClick] = [Action ListView_MouseDoubleClick($this, $eventArgs)]��5W      $��Center=�5X      ��5R      �� $ Results� �� SelectedItem 5U      �� $ SelectedResult���5[      �� 5\      .R�5]      �� ���#CCCCCC$��1q�63   ._�5^      �  5`      �� 5a   "   $
��auto �63   6C   �� 5b   "   $
��auto �63   6C   �� 5c   "   $
��auto �63   6C    5e      �� 5f   "   $��40 �60   6>   .+�5j      �� $��14(�5k   $   $��0��62   $��1��6B   $
��Bold"�5l   $   ��5j   $   �� *DisplaySymbol+5m      �� 5n      $��1��5o   $   $��0��64   $��1��6A   $��Light"�5p   $   ��5n   $   �� *Description+5r      5s      5t      5u      5w      5y      5{      