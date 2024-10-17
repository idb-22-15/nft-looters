import { Award, BriefcaseBusiness, Crown, FileBadge2, FileText, Settings, ShieldCheck } from 'lucide-vue-next'

export type NavItem = {
  icon: Component
  title: string
  url: string
}

export const userNavItems: NavItem[] = [
  {
    icon: BriefcaseBusiness,
    title: 'Организация',
    url: '/profile/user/organization',
  },
  {
    icon: Crown,
    title: 'Навыки',
    url: '/profile/user/skills',
  },
  {
    icon: Award,
    title: 'Достижения',
    url: '/profile/user/achievements',
  },
  {
    icon: FileBadge2,
    title: 'Дипломы',
    url: '/profile/user/diploma',
  },
  {
    icon: ShieldCheck,
    title: 'Сертификаты',
    url: '/profile/user/certificates',
  },
  {
    icon: FileText,
    title: 'Рекомендации',
    url: '/profile/user/recommendations',
  },
  {
    icon: Settings,
    title: 'Настройки',
    url: '/settings',
  },
]

export const organizationNavItems: NavItem[] = [
  {
    icon: BriefcaseBusiness,
    title: 'Организация',
    url: '/profile/organization/organization',
  },
  {
    icon: Award,
    title: 'Достижения',
    url: '/profile/organization/achievements',
  },
  {
    icon: FileBadge2,
    title: 'Дипломы',
    url: '/profile/organization/diploma',
  },
  {
    icon: ShieldCheck,
    title: 'Сертификаты',
    url: '/profile/organization/certificates',
  },
  {
    icon: FileText,
    title: 'Рекомендации',
    url: '/profile/organization/recommendations',
  },
  {
    icon: Settings,
    title: 'Настройки',
    url: '/settings',
  },
]
