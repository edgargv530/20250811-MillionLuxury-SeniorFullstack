import Header from './components/Header'
import Menu from './components/Menu'
import Footer from './components/Footer';
import Owners from './components/Owners';

function App() {
	return (
		<div className="container-fluid">
			<Header />

			{/* Área de trabajo */}
			<div className="row">
				{/* Menú lateral Izq. */}
				<Menu />

				{/* Cuerpo y carga de los componentes */}
				<main className="col ps-md-2 pt-2">
					<div className="row">
						<div className="col-12">
							<Owners />
						</div>
					</div>
				</main>
			</div>

			<Footer />
		</div>
	);
}

export default App