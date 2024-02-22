# Base
Chạy game từ Loading Scene

Unity Ver: 2020.3.38f1

Game hiện có 15 màn (sẽ còn add thêm), sau khi chạy hết màn cuối thì các màn kế sẽ được chọn ngẫu nhiên từ các màn có sẵn

Game có Inventory để đổi súng

Game có Shop để mua tiền in-game và mua thêm súng mới, hiện tại có các option cộng/trừ hoặc sửa tiền về 0 và reset tiến độ mua súng (mục đích để test tính năng game, sau này sẽ xóa)

Trên đường trong các màn hiện sẽ có các object sau:
- Money
- Cylinder (ổ đạn để bắn)
- Gate (Cổng thưởng sát thương, phải nạp đủ đạn thông qua ổ đạn để nhận thưởng)
- Gate 3 Lanes (Cổng thưởng loại 3 cửa, các cửa sẽ cho thưởng khi nạp đủ đạn)
- Spikes (Gai sẽ làm giảm 1% sát thương và đẩy người chơi lại)
- Bonus Panel (Bảng thưởng chỉ số, khi bắn vào sẽ cộng dồn vào số cuối cùng, khi người chơi chạm phải sẽ nhận được bonus theo số đó)
- Rock (Xuất hiện ở cuối mỗi màn, khi bắn vỡ sẽ rớt tiền cho người chơi nhặt, nếu va phải sẽ thua)

Ngoài ra các ổ đạn có thể di chuyển tiến lùi hoặc sang phải trái

Có các powerup bao gồm các súng có trong shop (khi nhặt trên đường thì sẽ chỉ được dùng tạm thời), dual wield (bắn 2 súng cùng lúc), triple wield (bắn 3 súng cùng lúc); Hiện các powerup này không được đặt trong các level (trừ một vài level đầu), script thì vẫn có nên có thể đặt vào các level để nhặt
