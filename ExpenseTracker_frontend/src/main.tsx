import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import Dashboard from './dashboard.tsx'
import { BrowserRouter, Routes, Route} from 'react-router'
import Accounts from './accounts.tsx'
import Transactions from './transactions.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
    <nav>
  <a href="/">Dashboard</a> |
  <a href="/accounts">Accounts</a> |
  <a href="/transactions/">Transactions</a>
</nav>
    <Routes>
    <Route path="/" element={<Dashboard />}/>
    <Route path="/accounts" element={<Accounts />}/>
    <Route path="/transactions" element={<Transactions />}/>
    </Routes>
    </BrowserRouter>
  </StrictMode>,
)
