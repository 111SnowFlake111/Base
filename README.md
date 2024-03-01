# Base
Chạy game từ Loading Scene

Unity Ver: 2020.3.38f1

Game hiện có 30 màn, sau khi chạy hết màn cuối thì các màn kế sẽ được chọn ngẫu nhiên từ các màn có sẵn

Trước khi bắt đầu chơi Player có thể tiêu tiền để nâng FireRate và Year

Game có Shop để mua tiền in-game, hiện tại có các option cộng/trừ hoặc sửa tiền về 0 và reset tiến độ lấy súng đặc biệt, reset stats (mục đích để test tính năng game, sau này sẽ xóa)

Cứ mỗi 10 Year thì súng sẽ được nâng cấp, súng mới sẽ có sát thương, tốc độ bắn và cự ly đạn bay xa hơn súng trước, nếu qua số lượng súng đang có thì súng sẽ không thay đổi nhưng Year vẫn sẽ tăng

Cuối đường sẽ có súng đặc biệt, các khẩu súng này sẽ có stats vượt trội hơn so với các súng lấy được thông qua Year

Các súng đặc biệt sẽ thay thế súng ở tay trái và giữa khi Player sử dụng Dual Wield và Triple Wield

Hiện tại chỉ có 2 súng đặc biệt, khi nhặt hết thì cuối đường sẽ spawn túi tiền (Money Bag), nhặt được sẽ nhận ngẫu nhiên 5000 - 10000$

Khi tới cuối đường hoặc va phải Rock (thua) thì game sẽ hiện Gacha, nhấn Claim sẽ nhận tiền thưởng nhân tương ứng, hoặc Player có thể nhấn No thanks và game sẽ tới màn hình kế

Sau màn hình Gacha là màn hình nâng Range và Damage, sau khi tiêu tiền để nâng (hoặc không) thì Player nhấn "Next Level" để chuyển màn kế

Trên đường trong các màn hiện sẽ có các object sau:
- Money: Nhặt được sẽ nhận ngẫu nhiên từ 40 - 60$
- Cylinder: ổ đạn để bắn, nhận tối đa 8 viên
- Gate: Cổng thưởng Year, phải nạp đủ đạn thông qua ổ đạn để nhận thưởng, mặc định sẽ thưởng 10 Year
- Gate 3 Lanes: Cổng thưởng loại 3 cửa, các cửa sẽ cho thưởng khi nạp đủ đạn, mặc định cửa 1 thưởng 4 Year, cửa 2 thưởng 8 Year, cửa 3 thưởng 12 Year 
- Spikes: Gai sẽ làm giảm 1 Year và đẩy người chơi lại, có thể sửa số lần va chạm, mặc định chỉ gây va chạm 1 lần
- Bonus Panel: Bảng thưởng chỉ số, khi bắn vào sẽ cộng dồn vào số cuối cùng, khi người chơi chạm phải sẽ nhận được bonus theo số đó, tên ghi trên cùng của bảng sẽ biểu thị loại thưởng mà Player sẽ nhận
- Rock: Xuất hiện ở cuối mỗi màn, khi bắn vỡ sẽ rớt tiền cho người chơi nhặt, nếu va phải sẽ thua
- Spreader: Khi bắn trúng sẽ bắn ra 2 viên đạn đi song song, hướng đi của 2 viên đạn có thể bao gồm thẳng thẳng/trái thẳng/thẳng phải/trái phải/custom)
- Spreader v2: Khi bắn trúng sẽ bắn đạn ra 6 hướng, Spreader này luôn luôn quay
- Clock: Hiện tại sẽ là bắn đủ số lần sẽ tăng Year tiếp nhận đạn liên tục cho tới khi Player va vào hoặc đi qua)
- Đường Speed Up/Down: Khi chạm vào tốc độ Player sẽ giảm/tăng, effect chỉ kích hoạt khi người chơi nằm trong vùng trigger của đường

Ngoài ra các ổ đạn và Bonus Panel có thể di chuyển tiến lùi hoặc sang phải trái. Bonus Panel có thể có tường chắn, hp của tường sẽ hiện ở trên đầu bảng, nếu Player va phải tường sẽ bị trừ 1 Year

Có các powerup bao gồm Dual Wield (bắn 2 súng cùng lúc), Triple Wield (bắn 3 súng cùng lúc); Hiện các powerup này không được đặt trong các level (trừ một vài level được chỉ định), script thì vẫn có nên có thể đặt vào các level để nhặt

***Demo bản cũ: https://youtu.be/HQQeqlNfzNU

***Demo bản mới: https://youtu.be/VThk3b5V0a0

*NOTE: Bản ở nhánh main hiện tại là bản stable, một bản overhaul lại game đang được thực hiện trong nhánh khác
