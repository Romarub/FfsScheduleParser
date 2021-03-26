# FfsScheduleParser
Parse json to csv for future use in VBA
Автоматизация руттинной работы по заполнению документов

Из веба запрашиваются json объекты (расписание сессий на 2 месяца).
Эти объекты фильтруются по заданному временному интервалу (в данном случае сессии за сутки от текущей даты). 
Отфильтрованные объекты записываются в csv файлы, с которыми потом удобно работать в VBA для автоматизации заполнения таблицы со статистикой и подготовки журнала сессий за 24 часа.

Таблица со статистикой заполняется в письме outlook. Журнал сессий за 24 часа распечатывается в word'e.

Пример заполнения таблицы в outlook:
<iframe width="560" height="315" src="https://www.youtube.com/embed/R6ZaBoLqzuc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
