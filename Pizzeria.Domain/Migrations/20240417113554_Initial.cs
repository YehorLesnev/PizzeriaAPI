using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    address1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    address2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    zipcode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    first_name = table.Column<string>(type: "varchar(55)", fixedLength: true, maxLength: 55, nullable: true),
                    last_name = table.Column<string>(type: "varchar(55)", fixedLength: true, maxLength: 55, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(25)", fixedLength: true, maxLength: 25, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ingredient_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ingredient_weight_measure = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ingredient_price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    quantity_in_stock = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredient_id);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    recipe_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    cooking_time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.recipe_id);
                });

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    shift_date = table.Column<DateOnly>(type: "date", nullable: false),
                    shift_start_time = table.Column<TimeOnly>(type: "time", nullable: false, defaultValue: new TimeOnly(8, 0, 0)),
                    shift_end_time = table.Column<TimeOnly>(type: "time", nullable: false, defaultValue: new TimeOnly(22, 0, 0))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.shift_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    first_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    last_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    position = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(25)", fixedLength: true, maxLength: 25, nullable: false),
                    hourly_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    item_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    item_category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    item_size = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    item_price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    image_path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.item_id);
                    table.ForeignKey(
                        name: "FK_items_recipes",
                        column: x => x.recipe_id,
                        principalTable: "recipes",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_ingredients",
                columns: table => new
                {
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_ingredients", x => new { x.recipe_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "FK_recipe_ingredients_ingredients",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "ingredient_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipe_ingredients_recipes",
                        column: x => x.recipe_id,
                        principalTable: "recipes",
                        principalColumn: "recipe_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    delivery = table.Column<bool>(type: "bit", nullable: false),
                    delivery_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_total = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_address",
                        column: x => x.delivery_address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "FK_orders_customers",
                        column: x => x.customer_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orders_staff",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                });

            migrationBuilder.CreateTable(
                name: "shift_staff",
                columns: table => new
                {
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_staff", x => new { x.shift_id, x.staff_id });
                    table.ForeignKey(
                        name: "FK_shift_staff_shift",
                        column: x => x.shift_id,
                        principalTable: "shifts",
                        principalColumn: "shift_id");
                    table.ForeignKey(
                        name: "FK_shift_staff_staff",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => new { x.order_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_order_items_items",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "FK_order_items_orders",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_items_recipe_id",
                table: "items",
                column: "recipe_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_items_item_id",
                table: "order_items",
                column: "item_id")
                .Annotation("SqlServer:Include", new[] { "order_id", "quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_date",
                table: "orders",
                column: "date")
                .Annotation("SqlServer:Include", new[] { "order_id", "customer_id", "staff_id" });

            migrationBuilder.CreateIndex(
                name: "IX_orders_delivery_address_id",
                table: "orders",
                column: "delivery_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_staff_id",
                table: "orders",
                column: "staff_id")
                .Annotation("SqlServer:Include", new[] { "order_id", "date", "customer_id" });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredients_ingredient_id",
                table: "recipe_ingredients",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_staff_staff_id",
                table: "shift_staff",
                column: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "recipe_ingredients");

            migrationBuilder.DropTable(
                name: "shift_staff");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "staff");
        }
    }
}
