   M S B A M L   `   `   `  ????  dGPT    dGPT.Settings   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   Bd2http://schemas.microsoft.com/expression/blend/2008      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          ?Name$  	SettingsF??5   	    ? Title$ DeskGPT??5   	    ? Topmost$
 True??65    ? WindowStartupLocation$ CenterScreen=?6D    ? ShowInTaskbar$ False??6i    ? AllowsTransparency$
 True??6   $	??663??6?   $	??340??6?    ? WindowStyle$
 None=?6?    ? 
ResizeMode$ NoResize=?6?   
?????? $??Bottom=?6?   .??5	      ?? ???	#FF395372
?????? $??1,1,1,1q?6>    ?? CornerRadius$ 15,15,15,15??6X   $	??NaN??6s   ._?5
   
   ? 
?????? 	 J? RenderTransformOrigin$	 0.5,0.5&?6(   $??	0,-1,0,-1q?6H   
 ? ColumnDefinitions
 5      z?5      V? 5      .~?5      ??  ?? ScaleY$ 1Q?6%   ?? 5      ?? 5      T? 5      5      .+?5      ? -   5        ???	#FF395372$??0,7,38,0q?5   E   $	 0.5,0.5&?6W   $??Right=?6w   $	??291??6?   $??37??6?   $	??Top=?6?   z?5      V? 5      .~?5      ?? $ 1Q?6)   ?? 5      ?? 5      T? 5      5      
 5      ?? 5      $??7* ?6'   ?? 5       $	??68* ?6'   ?? 5!      $
??228* ?6'   .+?5#      ?? -   5         ??Name$ Title??5#      $??Settings??6'   $??Center=?6:   
???????$??44??6h   $??2??6t   $
??Left=?6?   $	??100??6?   $??50,0,0,0q?6?   $??20(?6?   5$      ?? 5%      -   5           ButtonStyle1#???   5%      $??48??6<   $??48??6G   $
??Left=?6S   $	??Top=?6n   $??237,598,0,0q?6?   ???	#FF385372???	#FFABABAB ?? 	IsDefault$
 True??6?   $	 0.5,0.5&?6?    ? 	Resources 5&       ?? 
TargetType ?? Property ?? Value??%^   '??       ?? 5'      " ??6   .?5(      ??  ?@??CornerRadius$ 5??69   5)      5*      z?5+      V? 5,      .~?5-      ?? $ -1Q?6)   ?? 5.      ?? 5/      T? 50      51      .??53      ?? $??30??6   $??30??6#   $??
/reset.png??6/   $??Center=?6C   54      ?? 55      $??OpenAI API Key??6   $
??Left=?6-   $??2,33,0,0q?6H   $	??Top=?6Z   $??20(?6r   
???????6?   ?? 56      -   5         ??Name$
 apiT??56        TextBoxStyle2#???  6$   $??Center=?6L   $??0,70,0,0q?6i   $??20(?6{   $	??Top=?6?   $	??328??6?   $??36??6?   $	 0.5,0.5&?6?   ???	#FF385372???	#FF2C3D53
???????.??6   57      ??%^   '??       ?? 58      " ??6   .?59      ??  ?@??CornerRadius$ 5??69   5:      5;      .??6%   z?5<      V? 5=      .~?5>      ?? $ 1Q?6)   ?? 5?       ?? AngleX$ 1.023Q?6(   ?? 5@      T? 5A       T? X$ -0.232Q?6-   5B      .??5C   +   5D      ?? 5E      -   5        #???   5E      $??48??6<   $??48??6G   $
??Left=?6S   $	??Top=?6n   $??
56,598,0,0q?6?   ???	#FF385372???	#FF39CC98$
 True??6?   $	 0.5,0.5&?6?    5F      ??%^   '??       ?? 5G      " ??6   .?5H      ??  ?@??CornerRadius$ 5??69   5I      5J      z?5K      V? 5L      .~?5M      ?? $ -1Q?6)   ?? 5N      ?? 5O      T? 5P      5Q      .??5S      ?? $??30??6   $??30??6#   $??	/save.png??6/   $??Center=?6B   5T      ?? 5U      -   5         ??Name$ Startup??5U      $??Start when Windows starts??6(   $
??Left=?6L   $??
19,525,0,0q?6g   $	??304??6{   $	 0.5,0.5&?6?   $??Center=?6?   
??????? d? 	IsChecked$
 TrueY?6?   $??20(?6  $??31??6  $	??Top=?6"  z?5V      V? 5W      .~?5X      ?? ?? 5Y      $ 1.591Q?6(   ?? 5Z      T? 5[      $ 0.222Q?6-   5\      5^      ?? 5_      -   5         ??Name$ tempS??5_      $??Center=?6$   $??	0,143,0,0q?6A   $	??Top=?6T   $	??318??6l   
??????? ? Maximum$ 1Q?6?    ? Value$	 0.5Q?6?    ?? TickFrequency$
 0.05Q?6?    ?? IsSnapToTickEnabled$
 True??6?    ? SmallChange$
 0.05Q?64   ? LargeChange$
 0.05Q?6G  ?? 5`      $??Temperature??6   $
??Left=?6*   $??105,106,0,0q?6E   $	??Top=?6Z   $??20(?6r   
???????6?   ?? 5a      -	   5        $ freqS??5a      $??Center=?6$   $??	0,200,0,0q?6A   $	??Top=?6T   $	??318??6l   
???????$ 2Q?6?   $ 1Q?6?    ? Minimum$ -2Q?6?   $
 0.05Q?6  $
 0.05Q?6!  $
 True??64  $
 0.05Q?6O  ?? 5b      $??Frequency Penalty??6   $??	0,162,0,0q?60   $??20(?6C   
???????$	??Top=?6d   $??Center=?6|   ?? 5c      -
   5        $
 preS??5c      $??Center=?6#   $	??Top=?6@   $	??318??6X   
???????$ 2Q?6w   $ 2Q?6?   $ -2Q?6?   $
 0.05Q?6?   $
 0.05Q?6	  $
 0.05Q?6  $
 True??61  $??	0,262,0,0q?6L  ?? 5d      $??Presence Penalty??6   $??20(?6/   
???????$	??Top=?6P   $??Center=?6h   $??	0,225,0,0q?6?   ?? 5e      -   5        $	 boS??5e      $??Center=?6"   $??	0,313,0,0q?6?   $	??Top=?6R   $	??318??6j   
???????$ 5Q?6?   $ 2Q?6?   $ 1Q?6?   $ 1Q?6  $ 1Q?6  $
 True??6#  ?? 5f      $??Best Of??6   $??128,278,0,0q?6&   $??20(?6;   
???????$	??Top=?6\   $
??Left=?6t   ?? 5g      -   5        $ probS??5g      $??Center=?6$   $??	0,367,0,0q?6A   $	??Top=?6T   $	??318??6l   
???????$	 100Q?6?   $ 50Q?6?   $
 True??6  $ 1Q?6  $ 1Q?6-  ?? 5h      $??Top Probability??6   $??	0,332,0,0q?6.   $??20(?6A   
???????$	??Top=?6b   $??Center=?6z   ?? 5i      -   5        $ tempD??5i      $
??0.50??6#   $??Center=?62   $
??Left=?6V   $??262,110,0,0q?6q   $	??Top=?6?   $??18(?6?   
???????$??33??6?   $??61??6?   $y?Hidden=?6?   ?? 5j      -   5        $ freqD??5j      $??1??6#   $??Center=?6/   $
??Left=?6S   $??262,166,0,0q?6n   $	??Top=?6?   $??18(?6?   
???????$??34??6?   $??61??6?   $y?Hidden=?6?   ?? 5k      -   5        $
 preD??5k      $??2??6"   $??Center=?6.   $
??Left=?6R   $??262,225,0,0q?6m   $	??Top=?6?   $??18(?6?   
???????$??37??6?   $??61??6?   $y?Hidden=?6?   ?? 5l      -   5        $	 boD??5l      $??2??6!   $??Center=?6-   $
??Left=?6Q   $??262,276,0,0q?6l   $	??Top=?6?   $??18(?6?   
???????$??37??6?   $??61??6?   $y?Hidden=?6?   ?? 5m      -   5        $ probD??5m      $??50??6#   $??Center=?60   $
??Left=?6T   $??262,338,0,0q?6o   $	??Top=?6?   $??18(?6?   
???????$??31??6?   $??61??6?   $y?Hidden=?6?   ?? 5n      -   5         ??Name$ CloseB??5n   P   #???   6`   $??281,7,-11,0q?6?   $??38??6?   $	??Top=?6?   ??6?   Z? ??6?   Z? .??5o      ?? -   5          ??Name$  	CloseBBut??5o      $??20??6+   $??Center=?67   $	 0.5,0.5&?6R   $??17??6r   ??5p      ?? 5q      " ??6    .?5r      ??  ? ??$ imgs/closeI.png??67   ! ?? Triggers! 5s      P? 5t   "   " P? Property" ?@J?IsMouseOver# P? Value$
# True??6A   . ?5u   &   ??  ? ??$ imgs/closeH.png??6?   5v   #   5x      z?5z      V? 5{      .~?5|      ?? $ ?? ScaleX$$ 1Q?6-   ?? 5}      ?? 5~      T? 5      5?      5?      5?      ?? 5?      -   5        $	 Max??5?      $??Visible on start??6$   $
??Left=?6?   $??
19,556,0,0q?6Z   $	??304??6n   $	 0.5,0.5&?6?   $??Center=?6?   
???????$
 TrueY?6?   $??20(?6?   $??32??6	  $	??Top=?6  z?5?      V? 5?      .~?5?      ?? ?? 5?      $ 1.591Q?6(   ?? 5?      T? 5?      $ 0.222Q?6-   5?      5?      ?? 5?      -   5        % ??Name$% modelBox??5?        ComboBoxTemplate#???  6)   $
??Left=?6X   $??
17,484,0,0q?6s   $	??Top=?6?   $??20(?6?   $	??306??6?   ???	#FF2C3D53???	#FF385372
???????.@?5?      ?? & ?? ContentStringFormat$&  ??6   $??ChatGPT (GPT 3.5)??66   ' ?? 
IsSelected$
' True??6R   ?? 5?      $??Davinci (GPT 3)??6   5?      ?? 5?      $??Hotkey??6   $??	0,390,0,0q?6%   $??20(?68   
???????$	??Top=?6Y   $??Center=?6q   ?? 5?      -   5        $% modiBox??5?      #???  6(   $
??Left=?6W   $??
17,432,0,0q?6r   $	??Top=?6?   $??20(?6?   $	??134??6?   ???	#FF2C3D53???	#FF385372
???????.@?5?      ?? $&  ??6   $??WIN Key??66   $
' True??6H   ?? 5?      $??ALT Key??6   ?? 5?      $??CTRL Key??6   5?      ?? 5?      -   5        $% keyBox??5?      #???  6'   $
??Left=?6V   $??186,432,0,0q?6q   $	??Top=?6?   $??20(?6?   $	??137??6?   ???	#FF2C3D53???	#FF385372
???????.@?5?      ?? $&  ??6   $??A??66   $
' True??6B   ?? 5?      $??B??6   ?? 5?      $??C??6   $
' True??6+   ?? 5?      $??D??6   ?? 5?      $??E??6   ?? 5?      $??F??6   ?? 5?      $??G??6   ?? 5?      $??H??6   ?? 5?      $??I??6   ?? 5?      $??J??6   ?? 5?      $??K??6   ?? 5?      $??L??6   ?? 5?      $??M??6   ?? 5?      $??N??6   ?? 5?      $??O??6   ?? 5?      $??P??6   ?? 5?      $??Q??6   ?? 5?      $??R??6   ?? 5?      $??S??6   ?? 5?      $??T??6   ?? 5?      $??U??6   ?? 5?      $??V??6   ?? 5?      $??W??6   ?? 5?      $??X??6   ?? 5?      $??Y??6   ?? 5?      $??Z??6   5?      ?? 5?      $??+??6   $??	0,428,0,0q?6    $??20(?63   
???????$	??Top=?6T   $??Center=?6l   5?      5?      5?      