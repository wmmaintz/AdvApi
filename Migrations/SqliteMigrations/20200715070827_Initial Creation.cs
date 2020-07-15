using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvApi.Migrations.SqliteMigrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    IsOnLine = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Total = table.Column<decimal>(nullable: false),
                    Placed = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CustomerEmail = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerEmail",
                        column: x => x.CustomerEmail,
                        principalTable: "Customer",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "customer1@email.com", "Customer1", "Missouri" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "kbeasleigh20@hao123.com", "Kori Beasleigh", "Missouri" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jbignell1z@timesonline.co.uk", "Janel Bignell", "Massachusetts" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cninnoli1y@google.com.br", "Corey Ninnoli", "Minnesota" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ssidry1x@hp.com", "Sheppard Sidry", "Indiana" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "wpignon1w@woothemes.com", "Wilie Pignon", "Iowa" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "vcoping1v@jimdo.com", "Vance Coping", "District of Columbia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cmckiddin1u@google.ca", "Casey McKiddin", "Washington" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "areburn1t@theglobeandmail.com", "Adoree Reburn", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cdhennin1s@scientificamerican.com", "Cybil Dhennin", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "knaisey1r@exblog.jp", "Katerine Naisey", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "gde21@japanpost.jp", "Gar De Andisie", "Pennsylvania" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ncamps1q@apple.com", "Ninon Camps", "Illinois" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "borbell1o@epa.gov", "Ban Orbell", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "npicardo1n@hc360.com", "Nicoli Picardo", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "dbanbrigge1m@ft.com", "Dela Banbrigge", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "nbriddock1l@mlb.com", "Nessie Briddock", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "wsworn1k@dyndns.org", "Wilmer Sworn", "District of Columbia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cbuddington1j@indiegogo.com", "Cos Buddington", "Indiana" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cofeeney1i@vkontakte.ru", "Cordell O'Feeney", "Virginia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "csymper1h@mozilla.com", "Cyndia Symper", "Illinois" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "hjansens1f@reverbnation.com", "Hussein Jansens", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "bwinchcombe1e@flavors.me", "Berkie Winchcombe", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "vdeere1p@senate.gov", "Viva Deere", "Virginia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "htarrant22@yahoo.co.jp", "Hermina Tarrant", "Ohio" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jmaccoughen23@hud.gov", "Jaye MacCoughen", "Massachusetts" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "dtraylen24@spotify.com", "Dodi Traylen", "Arkansas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "fsheldrake2r@over-blog.com", "Forster Sheldrake", "Georgia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "fhardiker2q@google.com.au", "Forest Hardiker", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ycattel2p@istockphoto.com", "Yevette Cattel", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "lcurrier2o@goodreads.com", "Laird Currier", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "agriswaite2n@goo.ne.jp", "Addy Griswaite", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "agatch2m@discovery.com", "Avictor Gatch", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "bjaram2l@youku.com", "Bernadette Jaram", "District of Columbia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "pincogna2k@acquirethisname.com", "Peggi Incogna", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ltampion2j@topsy.com", "Linn Tampion", "Washington" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "rby2i@nih.gov", "Raddie By", "Oklahoma" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "tjoret2h@posterous.com", "Teddy Joret", "Missouri" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "kharrald2g@slate.com", "Kahaleel Harrald", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "osavil2f@lulu.com", "Olenolin Savil", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ahandrik2e@sourceforge.net", "Alic Handrik", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "rspratley2d@cdc.gov", "Ricky Spratley", "Colorado" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ceatttok2c@hubpages.com", "Crosby Eatttok", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "sleys2b@mozilla.org", "Scotti Leys", "North Carolina" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "skrates2a@theatlantic.com", "Salome Krates", "Ohio" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "rscouller29@salon.com", "Rosamond Scouller", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "mkefford28@barnesandnoble.com", "Michaeline Kefford", "Virginia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "fskim27@google.fr", "Franchot Skim", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ivigne26@ehow.com", "Irwin Vigne", "Connecticut" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "nflucker25@nasa.gov", "Natassia Flucker", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "oburras1d@slate.com", "Orly Burras", "Missouri" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ewise1c@hao123.com", "Elora Wise", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "mpagden1g@symantec.com", "Mildred Pagden", "District of Columbia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jquarry1a@pinterest.com", "Jinny Quarry", "Iowa" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "sleipoldtj@meetup.com", "Stinky Leipoldt", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "espottswood1b@ask.com", "Esdras Spottswood", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "evaillanth@so-net.ne.jp", "Erma Vaillant", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "tlauchlang@apache.org", "Tobiah Lauchlan", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "vsahnowf@msn.com", "Viva Sahnow", "New Mexico" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "hmcgarviee@ovh.net", "Hazel McGarvie", "Arkansas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "gdauneyd@lulu.com", "Gay Dauney", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "gportmanc@prweb.com", "Gertrud Portman", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "dbennetb@twitpic.com", "Dorene Bennet", "Wisconsin" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ddhoogea@state.tx.us", "Demetrius D'Hooge", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "lgilstin9@scribd.com", "Lesley Gilstin", "Colorado" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "nthamelt8@ycombinator.com", "Ninette Thamelt", "Virginia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "tmacias7@twitpic.com", "Tod Macias", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "iheyburn6@exblog.jp", "Isidro Heyburn", "Ohio" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "atripcony5@w3.org", "Alexina Tripcony", "Virginia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "lglasscoe4@ameblo.jp", "Leonora Glasscoe", "Minnesota" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "zneylan3@umich.edu", "Zachary Neylan", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "elehrian2@mediafire.com", "Eddy Lehrian", "Oklahoma" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "sdicks1@gov.uk", "Shea Dicks", "Pennsylvania" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ghabergham0@cornell.edu", "Gussie Habergham", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "customer4@email.com", "Customer4", "Kansas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "customer3@email.com", "Customer3", "Colorado" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "customer2@email.com", "Customer2", "Pennsylvania" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jkillbeyk@yolasite.com", "Jillian Killbey", "Ohio" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "olayingl@eventbrite.com", "Orren Laying", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "dgarrardi@ucla.edu", "Deonne Garrard", "California" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "tmeasorn@godaddy.com", "Terrijo Measor", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "avettore19@cbsnews.com", "Antonia Vettore", "Kansas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "scowndley18@de.vu", "Skelly Cowndley", "Pennsylvania" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "bhazard17@typepad.com", "Burch Hazard", "Illinois" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "sbernardini16@reference.com", "Silvio Bernardini", "Missouri" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "mkennion15@java.com", "Menard Kennion", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "asturdy14@taobao.com", "Angus Sturdy", "Kansas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "rgianneschim@stumbleupon.com", "Rudiger Gianneschi", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "mpatnelli12@vkontakte.ru", "Miranda Patnelli", "Maryland" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "cbigmore11@omniture.com", "Connie Bigmore", "District of Columbia" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "bfavey10@telegraph.co.uk", "Barbey Favey", "Kentucky" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jgubbinsz@msn.com", "Jedd Gubbins", "Alabama" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "tduce13@tinyurl.com", "Tallie Duce", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "jmilwardx@yelp.com", "Jedd Milward", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ddelahunto@blogger.com", "Dunstan Delahunt", "Oklahoma" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "mruckerty@wiley.com", "Mannie Ruckert", "Oregon" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "gwillattsq@shutterfly.com", "Gunar Willatts", "Florida" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ajaquinr@twitpic.com", "Anastasie Jaquin", "Pennsylvania" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "bcoattsp@cisco.com", "Barclay Coatts", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "nscedallt@howstuffworks.com", "Nickolas Scedall", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "echmielu@google.it", "Eve Chmiel", "Michigan" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "dmathiasenv@nhs.uk", "Douglass Mathiasen", "New York" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "ggergusw@opera.com", "Giovanna Gergus", "Texas" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Email", "Name", "State" },
                values: new object[] { "gjeffrays@amazon.co.uk", "Gisele Jeffray", "Massachusetts" });

            migrationBuilder.InsertData(
                table: "Servers",
                column: "Name",
                value: "PROD-SrvrApp");

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "DEV-SrvrWeb", true });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "DEV-SrvrApp", true });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "DEV-SrvrRdb", true });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "QA-SrvrWeb", true });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "QA-SrvrApp", true });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Name", "IsOnLine" },
                values: new object[] { "QA-SrvrRdb", true });

            migrationBuilder.InsertData(
                table: "Servers",
                column: "Name",
                value: "PROD-SrvrWeb");

            migrationBuilder.InsertData(
                table: "Servers",
                column: "Name",
                value: "PROD-SrvrRdb");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Completed", "CustomerEmail", "Placed", "Status", "Total" },
                values: new object[] { 1, new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer1@email.com", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Open", 100m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Completed", "CustomerEmail", "Placed", "Status", "Total" },
                values: new object[] { 2, new DateTime(2020, 3, 3, 16, 0, 0, 0, DateTimeKind.Unspecified), "customer2@email.com", new DateTime(2020, 2, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), "Complete", 200m });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerEmail",
                table: "Orders",
                column: "CustomerEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
