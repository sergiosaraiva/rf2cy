describe('*** Settings ***', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Documentation A resource file with reusable keywords and variables.

})

describe('...', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('... The system specific keywords created here form our own', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('... domain specific language.They utilize keywords provided', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('... by the imported SeleniumLibrary.', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Library SeleniumLibrary

})

describe('*** Variables ***', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${SERVER} localhost: 7272', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${BROWSER} Firefox', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${DELAY} 0', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${VALID USER} demo', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${VALID PASSWORD} mode', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${LOGIN URL} http://${SERVER}/', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${WELCOME URL} http://${SERVER}/welcome.html', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('${ERROR URL} http://${SERVER}/error.html', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Keywords ***', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Open Browser To Login Page', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Open Browser ${LOGIN URL} ${BROWSER}', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Maximize Browser Window', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Set Selenium Speed ${DELAY}', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('*** Login Page Should Be Open', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    
})

describe('Login Page Should Be Open', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Title Should Be Login Page
		it('TitleShouldBe', () => {
			cy.title().should('eq', 'Login Page')
		})


})

describe('Go To Login Page', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Go To ${LOGIN URL}
		cy.visit('${LOGIN URL}')

		// Login Page Should Be Open

})

describe('Input Username', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// [Arguments] ${username}
		// Input Text username_field ${username}

})

describe('Input Password', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// [Arguments] ${password}
		// Input Text password_field ${password}

})

describe('Submit Credentials', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Click Button login_button

})

describe('Welcome Page Should Be Open', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    		// Location Should Be ${WELCOME URL}
		// Title Should Be Welcome Page
		it('TitleShouldBe', () => {
			cy.title().should('eq', 'Welcome Page')
		})


})

