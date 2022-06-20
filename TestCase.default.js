describe('//##TEST_CASE_NAME##', () => {

    beforeEach(() => {
        cy.signIn(Cypress.env('adminUser'), Cypress.env('adminPass'))
        cy.visit('configuration/accountsflows')
    })

    afterEach(() => {
        cy.signOut()
    })

    //##TEST_STEPS##
})

