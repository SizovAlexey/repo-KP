   M S B A M L   `   `   `  ����F  BCarShowLada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CarShowLada.PagesCarShowLada.Pages  5      !    CarShowLada.Pages.SalesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:CarShowLada.Pages     5      -   5          N� Title$  Продажи��5	      .��5      � -   5        $��GridPage��5       � RowDefinitions 5   
   �� 5      $��34 �6   6*   �� 5      $��* �6   6)   �� 5      $��50 �6   6*   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� $
��auto��6      BlockCommon#��[  6%   $��Сортировка��6J   $
��Bold"�6\   $��Center=�6n   $��7 0 5 0q�6�   6�   �� 5      -   5        $��ComboSortByDateSale��5   .   $��5q�6   $	��200��6"   .@�5      �� $*��$По возрастанию даты��6   6?   �� 5      $$��По убыванию даты��6   6<   5      5      �� 5   
   -   5        $��
LViewSales��5       $��1��6   $��Disabled=�62   ��5      �� 5       �� ImageSource$! /Assets/lada-background.jpg��6   6I   ��5      �� 5      .R�5      �  � Orientation$ 
Horizontal=�6    $��Center=�69   6X   5      ��5!      �� 5"      .R�5#      ��   ColorAccent#��[ 6    �� CornerRadius$ 15��6B    �� Padding$ 10q�6T   ���#75c1ff._�5$      � .+�5%      �� $	��500��6)   .+�5&   "   �� $ 
Horizontal=�6-   .+�5'   &   �� #��[  60   $
��auto��6U   $��0 0 7 0q�6�   $
��Bold"�6�   ��6b   �� *DateSaleText+6�   5(   #   �� 5)   "   $ 
Horizontal=�6-   .+�5*   &   �� #��[  60   $
��auto��6U   ��6b   �� *
BranchText+6�   5+   #   �� 5,   "   $ 
Horizontal=�6-   .+�5-   &   �� #��[  60   $
��auto��6U   ��6b   �� *
WorkerText+6�   5.   #   �� 5/   "   $ 
Horizontal=�6-   .+�50   &   �� #��[  60   $
��auto��6U   ��6b   �� *
ClientText+6�   51   #   �� 52   "   $ 
Horizontal=�6-   .+�53   &   �� #��[  60   $
��auto��6U   ��6b   �� *	CarText+6}   54   #   �� 55   "   $ 
Horizontal=�6-   .+�56   &   �� $��	BtnDelete��6?   -   5        $��Удалить��56   -   y�6h   �� *AdminControlsVisibility+6�   57   #   58      59      5:      5;      5=      �� 5>   
   -   5        $��BtnPrint��5>   9   $��2��6   $
��Left=�6   $��Печать��6`   6s   �� 5?   
   -   5        $��
BtnAddSale��5?   :   $��2��6   $��Right=�6   $#��Добавить запись��6e   6�   5@      5A      