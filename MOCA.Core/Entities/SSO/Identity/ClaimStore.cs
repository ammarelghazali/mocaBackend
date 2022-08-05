using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.SSO.Identity
{
    public class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("All Locations", "View"),

            new Claim("Assets", "Add"),
            new Claim("Assets", "Edit"),
            new Claim("Assets", "View"),
            new Claim("Assets", "Delete"),

            new Claim("*/*/*/*/*/", "-------------------"),

            new Claim("Assigned Locations", "View"),

            new Claim("Contact info", "View"),

            new Claim("Eventspaces", "Add"),
            new Claim("Eventspaces", "Delete"),
            new Claim("Eventspaces", "View"),
            new Claim("Eventspaces", "Edit"),

            new Claim("Legal and Finance Details", "View"),

            new Claim("Locations", "View"),
            new Claim("Locations", "Add"),
            new Claim("Locations", "Edit"),
            new Claim("Locations", "Delete"),

            new Claim("LOS Memberships", "Add"),
            new Claim("LOS Memberships", "View"),
            new Claim("LOS Memberships", "Edit"),
            new Claim("LOS Memberships", "Delete"),

            new Claim("LOS Spaces", "Add"),
            new Claim("LOS Spaces", "View"),
            new Claim("LOS Spaces", "Edit"),
            new Claim("LOS Spaces", "Delete"),

            new Claim("Meeting Rooms", "Add"),
            new Claim("Meeting Rooms", "View"),
            new Claim("Meeting Rooms", "Edit"),
            new Claim("Meeting Rooms", "Delete"),

            new Claim("Membership Pricing", "Add"),
            new Claim("Membership Pricing", "Edit"),
            new Claim("Membership Pricing", "View"),
            new Claim("Membership Pricing", "Delete"),

            new Claim("Membership Benefit", "Add"),
            new Claim("Membership Benefit", "Edit"),
            new Claim("Membership Benefit", "View"),
            new Claim("Membership Benefit", "Delete"),

            new Claim("Property Database", "View"),

            new Claim("Property Managment Dynamic Lists", "Add"),
            new Claim("Property Managment Dynamic Lists", "Edit"),
            new Claim("Property Managment Dynamic Lists", "View"),
            new Claim("Property Managment Dynamic Lists", "Delete"),

            new Claim("Reports", "Extract"),
            new Claim("Reports", "View"),
            new Claim("Reports", "Edit"),

            new Claim("Restaurant Reservation", "Delete"),
            new Claim("Restaurant Reservation", "Edit"),
            new Claim("Restaurant Reservation", "View"),
            new Claim("Restaurant Reservation", "Add"),

            new Claim("Roles", "Add"),
            new Claim("Roles", "View"),
            new Claim("Roles", "Edit"),
            new Claim("Roles", "Delete"),

            new Claim("System Settings Dynamic Lists", "Add"),
            new Claim("System Settings Dynamic Lists", "Delete"),
            new Claim("System Settings Dynamic Lists", "View"),
            new Claim("System Settings Dynamic Lists", "Edit"),

            new Claim("Users", "Activate/Deactivate"),
            new Claim("Users", "Delete"),
            new Claim("Users", "Edit"),
            new Claim("Users", "View"),
            new Claim("Users", "Add"),

            new Claim("Workspaces", "Add"),
            new Claim("Workspaces", "View"),
            new Claim("Workspaces", "Delete"),
            new Claim("Workspaces", "Edit"),
        };
    }
}
