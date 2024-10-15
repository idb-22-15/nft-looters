import { Award, BriefcaseBusiness, Crown, FileBadge2, FileText, Settings, ShieldCheck } from 'lucide-vue-next'

export type NavItem = {
  icon: Component
  title: string
  url: string
}

export const navItems: NavItem[] = [
  {
    icon: BriefcaseBusiness,
    title: 'Организация',
    url: '/profile/organization',
  },
  {
    icon: Crown,
    title: 'Навыки',
    url: '/profile/skills',
  },
  {
    icon: Award,
    title: 'Достижения',
    url: '/profile/achievements',
  },
  {
    icon: FileBadge2,
    title: 'Дипломы',
    url: '/profile/diploma',
  },
  {
    icon: ShieldCheck,
    title: 'Сертификаты',
    url: '/profile/certificates',
  },
  {
    icon: FileText,
    title: 'Рекомендации',
    url: '/profile/recommendations',
  },
  {
    icon: Settings,
    title: 'Настройки',
    url: '/settings',
  },
]
