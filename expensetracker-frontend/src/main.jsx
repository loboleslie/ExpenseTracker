import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import Dashboard from './dashboard.jsx';
import { BrowserRouter, Routes, Route} from 'react-router';
import Accounts from './components/account/accounts.jsx';
import Transactions from './transactions.jsx';
import { Provider } from 'react-redux';
import { store } from './redux/store'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
    <Provider store={store}>
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
    </Provider>
    </BrowserRouter>
  </StrictMode>
)
