   M S B A M L   `   `   `  ����F  BCarShowLada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CarShowLada.PagesCarShowLada.Pages  5      !    CarShowLada.Pages.PostsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:CarShowLada.Pages     5      -   5          N� Title$  Должности��5	      .��5      � -   5        $��GridPage��5       � RowDefinitions 5   
   �� 5      $��34 �6   6*   �� 5      $��* �6   6)   �� 5      $��50 �6   6*   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� $
��auto��6      BlockCommon#��[  6%   $��Сортировка��6J   $
��Bold"�6\   $��Center=�6n   $��7 0 5 0q�6�   6�   �� 5      -   5        $��ComboSortBySalary��5   .   $��5q�6   $	��230��6"   .@�5      �� $2��,По возрастанию зарплаты��6   6C   �� 5      $,��&По убыванию зарплаты��6   6@   5      �� 5      $
��auto��6   #��[  6%   $��
Поиск��6J   $
��Bold"�6W   $��Center=�6i   $��7 0 5 0q�6�   6�   �� 5      -   5        $��
TBoxSearch��5      $	��150��6(   $��5q�64   6f   5      �� 5   
   -   5        $��
LViewPosts��5       $��1��6   $��Disabled=�62   ��5      �� 5       �� ImageSource$! /Assets/lada-background.jpg��6   6I   ��5      �� 5      .R�5       �  � Orientation$ 
Horizontal=�6    $��Center=�69   6X   5!      ��5#      �� 5$      .R�5%      ��   ColorAccent#��[ 6    �� CornerRadius$ 15��6B    �� Padding$ 10q�6T   ���#75c1ff._�5&      � .+�5'      �� $	��400��6)   .+�5(   "   �� $ 
Horizontal=�6-   .+�5)   &   �� #��[  60   $
��auto��6U   $��0 0 7 0q�6}   $
��Bold"�6�   ��6b   �� *	Name_post+6�   5*   #   �� 5+   "   $ 
Horizontal=�6-   .+�5,   &   �� #��[  60   $
��auto��6U   $��0 0 7 0q�6~   ��6b   �� *
SalaryText+6�   5-   #   �� 5.   "   $ 
Horizontal=�6-   .+�5/   &   �� $��BtnEdit��6E   -   5        $ ��Редактировать��5/   -   y�6j   �� *AdminControlsVisibility+6�   �� 50   &   $��	BtnDelete��6?   -   5        $��Удалить��50   -   y�6h   �� *AdminControlsVisibility+6�   51   #   52      53      54      55      57      �� 58   
   -   5        $��BtnPrint��58   9   $��2��6   $
��Left=�6   $��Печать��6`   6s   �� 59   
   -	   5        $��
BtnAddPost��59   :   $��2��6   $��Right=�6   $)��#Добавить должность��6e   6�   5:      5;      