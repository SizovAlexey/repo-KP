   M S B A M L   `   `   `  ����F  BCarShowLada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CarShowLada.PagesCarShowLada.Pages  5      #    CarShowLada.Pages.WorkersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:CarShowLada.Pages     5      -   5          N� Title$  Сотрудники��5	      .��5      � -   5        $��GridPage��5       � RowDefinitions 5   
   �� 5      $��34 �6   6*   �� 5      $��* �6   6)   �� 5      $��50 �6   6*   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� $
��auto��6      BlockCommon#��[  6%   $��Сортировка��6J   $
��Bold"�6\   $��Center=�6n   $��7 0 5 0q�6�   6�   �� 5      -   5        $ ��ComboSortByBirthdateWorker��5   .   $��5q�6   $	��280��6"   .@�5      �� $;��5По возрастанию даты рождения��6   6H   �� 5      $5��/По убыванию даты рождения��6   6E   5      �� 5      $
��auto��6   #��[  6%   $��
Поиск��6J   $
��Bold"�6W   $��Center=�6i   $��7 0 5 0q�6�   6�   �� 5      -   5        $��
TBoxSearch��5      $	��150��6(   $��5q�64   6f   5      �� 5   
   -   5        $��LViewWorkers��5       $��1��6   $��Disabled=�64   ��5      �� 5       �� ImageSource$! /Assets/lada-background.jpg��6   6I   ��5      �� 5      .R�5       �  � Orientation$ 
Horizontal=�6    $��Center=�69   6X   5!      ��5#      �� 5$      .R�5%      ��   ColorAccent#��[ 6    �� CornerRadius$ 15��6B    �� Padding$ 10q�6T   ���#75c1ff._�5&      �  � ColumnDefinitions 5'      �� 5(   "   $	��300 �63   6A   �� 5)   "   $	��400 �63   6A   .+�5+      �� $��0 0 7 0q�6D   ��6$   �� *Image_worker+6W   �� 5,      $��1��6)   .+�5-   "   �� $ 
Horizontal=�6-   .+�5.   &   �� #��[  60   $
��auto��6U   $��0 0 7 0q�6�   $
��Bold"�6�   ��6b   �� *Surname_worker+6�   �� 5/   &   #��[  60   $
��auto��6U   $��0 0 7 0q�6   $
��Bold"�6�   ��6b   �� *Name_worker+6�   �� 50   &   #��[  60   $
��auto��6U   $��0 0 7 0q�6�   $
��Bold"�6�   ��6b   �� *Patronymic_worker+6�   51   #   �� 52   "   $ 
Horizontal=�6-   .+�53   &   �� #��[  60   $
��auto��6U   ��6b   �� *BirthdateWorkerText+6�   54   #   �� 55   "   $ 
Horizontal=�6-   .+�56   &   �� #��[  60   $
��auto��6U   ��6b   �� *PostWorkerText+6�   57   #   �� 58   "   $ 
Horizontal=�6-   .+�59   &   �� #��[  60   $
��auto��6U   ��6b   �� *NumberPhoneText+6�   5:   #   �� 5;   "   $ 
Horizontal=�6-   .+�5<   &   �� $��BtnEdit��6E   -   5        $ ��Редактировать��5<   -   y�6j   �� *AdminControlsVisibility+6�   �� 5=   &   $��	BtnDelete��6?   -   5        $��Удалить��5=   -   y�6h   �� *AdminControlsVisibility+6�   5>   #   5?      5@      5A      5B      5D      �� 5E   
   -   5        $��BtnPrint��5E   9   $��3��6   $
��Left=�6   $��Печать��6`   6s   �� 5F   
   -	   5        $��BtnAddWorker��5F   :   $��2��6   $��Right=�6   $+��%Добавить сотрудника��6i   6�   5G      5H      