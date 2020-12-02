select
    D.Name as 'Документ',
    D.Pages as 'Страницы',
    concat(Sender.Fullname,', ', SenderPosition.Name)  as 'Отправитель: ФИО, должность',
    concat(SenderCity.Name, ', ул. ', SenderAddress.Street, ', д. ',SenderAddress.House) as 'Отправитель: адрес',
    concat(Receiver.Fullname, ', ', ReceiverPosition.Name) as 'Получатель: ФИО, должность',
    concat(ReceiverCity.Name, ', ул. ', ReceiverAddress.Street, ', д. ',ReceiverAddress.House) as 'Получатель: адрес',
    string_agg(concat(DS.DateTime, ' ', S.Name), char(13)) as 'Статус, дата'
from [DocumentStatus] as DS
    join [Document] as D
     on DS.DocumentId = D.Id
    join [Employee] as Sender
     on DS.SenderId = Sender.Id
    join [Address] as SenderAddress
     on DS.SenderAddressId = SenderAddress.Id
    join [City] as SenderCity
     on SenderAddress.CityId = SenderCity.Id
    join [Position] as SenderPosition
     on Sender.PositionId = SenderPosition.Id
    join [Employee] as Receiver
     on DS.ReceiverId = Receiver.Id
    join [Address] as ReceiverAddress
     on DS.ReceiverAddressId = ReceiverAddress.Id
    join [City] as ReceiverCity
     on ReceiverAddress.CityId = ReceiverCity.Id
    join [Position] as ReceiverPosition
     on Receiver.PositionId = ReceiverPosition.Id
    join [Status] as S
     on DS.StatusId = S.Id
group by D.Name,
         D.Pages,
         Sender.Fullname,
         SenderPosition.Name,
         concat(SenderCity.Name, ', ул. ', SenderAddress.Street, ', д. ',SenderAddress.House),
         Receiver.Fullname,
         ReceiverPosition.Name,
         concat(ReceiverCity.Name, ', ул. ', ReceiverAddress.Street, ', д. ',ReceiverAddress.House)