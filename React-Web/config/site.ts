export type SiteConfig = typeof siteConfig;

export const siteConfig = {
	name: "Pizzeria Shon",
	description: "Enjoy the best meals!",
	navItems: [
		{
			label: "Home",
			href: "/",
		},
    {
      label: "Docs",
      href: "/fries",
    },
    {
      label: "Pricing",
      href: "/burgers",
    },
    {
      label: "Blog",
      href: "/hotdogs",
    },
    {
      label: "About",
      href: "/pizza",
    }
	],
	navMenuItems: [
		{
			label: "Profile",
			href: "/profile",
		},
		{
			label: "Dashboard",
			href: "/dashboard",
		},
		{
			label: "Projects",
			href: "/projects",
		},
		{
			label: "Team",
			href: "/team",
		},
		{
			label: "Calendar",
			href: "/calendar",
		},
		{
			label: "Settings",
			href: "/settings",
		},
		{
			label: "Help & Feedback",
			href: "/help-feedback",
		},
		{
			label: "Logout",
			href: "/logout",
		},
	],
	links: {
		github: "https://github.com/YehorLesnev/PizzeriaAPI",
		twitter: "https://twitter.com/",
		docs: "https://nextui.org",
		discord: "https://discord.gg/",
    sponsor: "https://github.com/YehorLesnev/PizzeriaAPI"
	},
};
