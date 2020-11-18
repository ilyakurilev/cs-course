create table DepartureBoard(
    id int not null,
    FromCountry varchar(64) not null,
    FromCity varchar(64) not null,
    ToCountry varchar(64) not null,
    ToCity varchar(64) not null,
    DepartureDate datetimeoffset not null,
    ArrivalDate datetimeoffset not null,
    FlightTime time not null,
    Airline varchar(64) not null,
    AirplaneModel varchar(32) not null
)

insert into DepartureBoard(
    id,
    FromCountry,
    FromCity,
    ToCountry,
    ToCity,
    DepartureDate,
    ArrivalDate,
    FlightTime,
    Airline,
    AirplaneModel
)
values
(0, 'Russia', 'Anapa', 'Indonesia', 'Bali', '2020-12-12 23:40:00 +03:00', '2020-12-13 22:35:00 +08:00', '19:55:00', 'S7', 'Boeing 737-800'),
(1, 'Россия', 'Москва', 'Россия', 'Санкт-Петербург', '2020-12-14 12:00:00 +03:00', '2020-12-14 13:40:00 +03:00', '01:40:00', 'Аэрофлот', 'Airbus а330-200')

select * from DepartureBoard

drop table DepartureBoard